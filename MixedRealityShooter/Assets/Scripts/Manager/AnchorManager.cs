using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Meta.WitAi.Json;
using UnityEngine;
using Utility;

namespace Manager
{
    public class AnchorManager : Singleton<AnchorManager>
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _wallPrefab;
        [SerializeField] private GameObject _innerPrefab;
        private OVRSpatialAnchor _workingAnchor;
        private List<OVRSpatialAnchor> _allSavedAnchors;
        private List<OVRSpatialAnchor> _allRunningAnchors;
        
        private int _anchorSavedUUIDListSize;
        private const int _anchorSavedUUIDListMaxSize = 50;
        private Guid[] _anchorSavedUUIDList;
        private string _filePathSavedObjs;

        private List<SPLacedObject> _placedObjects;
        private List<SPLacedObject> _loadedPlacedObjects;

        public List<SPLacedObject> PlacedObjects => _placedObjects;
        
        private Action<OVRSpatialAnchor.UnboundAnchor, bool> _onLoadAnchor;

        protected override void Awake()
        {
            base.Awake();
            _allSavedAnchors = new List<OVRSpatialAnchor>();
            _allRunningAnchors = new List<OVRSpatialAnchor>();
            _placedObjects = new List<SPLacedObject>();
            _anchorSavedUUIDList = new Guid[_anchorSavedUUIDListMaxSize];
            _anchorSavedUUIDListSize = 0;
            _filePathSavedObjs = $"{Application.dataPath}/mrobjs.json";
            _onLoadAnchor = OnLocalized;
        }

        /// <summary>
        /// Checks if a file for the Anchors does exist and
        /// adds the UUIDS to an Array that is used to find Anchors on the HMD
        /// </summary>
        /// <returns>If a file was found or not</returns>
        public bool CheckForAnchors()
        {
            if (!File.Exists(_filePathSavedObjs))return false;

            string json = string.Empty;
            
            using (StreamReader reader = File.OpenText(_filePathSavedObjs))
            {
                json = reader.ReadToEnd();
            }
            
            _loadedPlacedObjects = JsonConvert.DeserializeObject<List<SPLacedObject>>(json);

            for (int i = 0; i < _loadedPlacedObjects.Count; i++)
            {
                _anchorSavedUUIDList[i] = _loadedPlacedObjects[i].UniqueId;
                _anchorSavedUUIDListSize = i;
            }

            return true;
        }

        public void SaveAllAnchors()
        {
            var objList = GameManager.Instance.MrPlacedObjects;
            
            foreach (var go in objList)
            {
                var tmp = new SPLacedObject
                {
                    UniqueId = new Guid(),
                    IsWall = go.CompareTag("Wall"),
                    Scaling = go.transform.localScale
                };
                CreateAnchor(go, tmp);
                _placedObjects.Add(tmp);
            }
            
            string json = JsonConvert.SerializeObject(_placedObjects);
            
            using (StreamWriter writer = File.CreateText(_filePathSavedObjs))
            {
                writer.Write(json);
            }
        }

        public void CreateAnchor(GameObject usedObj, SPLacedObject saveStruct)
        {
            _workingAnchor = usedObj.AddComponent<OVRSpatialAnchor>();
            StartCoroutine(AnchorCreated(_workingAnchor, saveStruct));
        }

        private IEnumerator AnchorCreated(OVRSpatialAnchor anchor, SPLacedObject saveStruct)
        {
            while (!anchor.Created && !anchor.Localized)
            {
                yield return new WaitForEndOfFrame();
            }
            
            _allRunningAnchors.Add(anchor);
            
            anchor.Save((anch, success) =>
            {
                if (success)
                {
                    _allSavedAnchors.Add(anch);
                    saveStruct.UniqueId = anch.Uuid;
                }
            });
        }

        public void LoadAllAnchors()
        {
             OVRSpatialAnchor.LoadOptions options = new OVRSpatialAnchor.LoadOptions
             {
                 Timeout = 0,
                 StorageLocation = OVRSpace.StorageLocation.Local,
                 Uuids = GetLocallySavedAnchorsUuids()
             };
            
             OVRSpatialAnchor.LoadUnboundAnchors(options, _anchorSavedUUIDList =>
             {
                 if (_anchorSavedUUIDList == null) return;

                 for (int i = 0; i < _anchorSavedUUIDList.Length; i++)
                 {
                     if (_anchorSavedUUIDList[i].Localized)
                     {
                         _onLoadAnchor(_anchorSavedUUIDList[i], true);
                     }
                     else if (!_anchorSavedUUIDList[i].Localizing)
                     {
                         _anchorSavedUUIDList[i].Localize(_onLoadAnchor);
                     }
                 }
             });
        }
        
        private Guid[] GetRuntimeSavedAnchorsUuids()
        {
            var uuids = new Guid[_allSavedAnchors.Count];
            using (var enumerator = _allSavedAnchors.GetEnumerator())
            {
                int i = 0;
                while (enumerator.MoveNext())
                {
                    var currentUuid = enumerator.Current.Uuid;
                    uuids[i] = new Guid(currentUuid.ToByteArray());
                    i++;
                }
            }
            return uuids;
        }

        private Guid[] GetLocallySavedAnchorsUuids()
        {
            if (CheckForAnchors())
            {
                return _anchorSavedUUIDList;
            }

            return GetRuntimeSavedAnchorsUuids();
        }
        
        private void OnLocalized(OVRSpatialAnchor.UnboundAnchor unboundAnchor, bool success)
        {
            var pose = unboundAnchor.Pose;
            SPLacedObject tmp = new SPLacedObject();

            foreach (var placedObject in _loadedPlacedObjects)
            {
                if (unboundAnchor.Uuid == placedObject.UniqueId)
                {
                    tmp = placedObject;
                    break;
                }
            }

            GameObject go;

            if (tmp.IsWall)
            {
                go = Instantiate(_wallPrefab, pose.position, pose.rotation);
            }
            else
            {
                go = Instantiate(_innerPrefab, pose.position, pose.rotation);
            }

            go.transform.localScale = tmp.Scaling;
            
            go.layer = LayerMask.NameToLayer("Environment");
            go.transform.GetChild(0).transform.gameObject.layer =
                LayerMask.NameToLayer("Environment");
            
            _workingAnchor = go.AddComponent<OVRSpatialAnchor>();
            unboundAnchor.BindTo(_workingAnchor);
            _allRunningAnchors.Add(_workingAnchor);
            GameManager.Instance.MrPlacedObjects.Add(go);
        }

        public void EraseAllAnchors()
        {
            foreach (var tmpAnchor in _allSavedAnchors)
            {
                if (tmpAnchor)
                {
                    //use a Unity coroutine to manage the async save
                    StartCoroutine(AnchorErased(tmpAnchor));
                }
            }

            _allSavedAnchors.Clear();
        }

        private IEnumerator AnchorErased(OVRSpatialAnchor osAnchor)
        {
            while (!osAnchor.Created)
            {
                yield return new WaitForEndOfFrame();
            }

            osAnchor.Erase((anchor, success) =>
            {
                if (!success)
                {
                    Debug.LogWarning("Anchor " + osAnchor.Uuid.ToString() + " NOT Erased!");
                }
                return;
            });
        }
    }
}
