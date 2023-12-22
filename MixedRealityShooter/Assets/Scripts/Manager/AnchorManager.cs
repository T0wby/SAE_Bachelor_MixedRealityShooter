using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utility;
using Newtonsoft.Json;

namespace Manager
{
    public class AnchorManager : Singleton<AnchorManager>
    {
        private const string NumUuidsPlayerPref = "numUuids";
        
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

        #region JSONList

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
        
        private void SaveAnchorUuidToExternalStore(OVRSpatialAnchor spAnchor)
        {
            var go = spAnchor.gameObject;
            var tmp = new SPLacedObject
            {
                UniqueId = spAnchor.Uuid
            };
            if (go.CompareTag("Wall"))
            {
                tmp.IsWall = true;
                tmp.ScalingX = go.transform.localScale.x;
                tmp.ScalingY = go.transform.localScale.y;
                tmp.ScalingZ = go.transform.localScale.z;
            }
            else
            {
                tmp.IsWall = false;
                tmp.ScalingX = go.transform.GetChild(0).localScale.x;
                tmp.ScalingY = go.transform.GetChild(0).localScale.y;
                tmp.ScalingZ = go.transform.GetChild(0).localScale.z;
            }
            _placedObjects.Add(tmp);
            
            string json = JsonConvert.SerializeObject(_placedObjects);
            
            using (StreamWriter writer = File.CreateText(_filePathSavedObjs))
            {
                writer.Write(json);
            }
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
        
        private Guid[] GetLocallySavedAnchorsUuids()
        {
            if (CheckForAnchors())
            {
                return _anchorSavedUUIDList;
            }
        
            return GetRuntimeSavedAnchorsUuids();
        }

        #endregion

        public bool CheckForAnchorsPrefs()
        {
            if (!PlayerPrefs.HasKey(NumUuidsPlayerPref))
            {
                PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
            }

            return PlayerPrefs.GetInt(NumUuidsPlayerPref) > 0;
        }

        public void SaveAllAnchors()
        {
            var objList = GameManager.Instance.MrPlacedObjects;
            
            foreach (var go in objList)
            {
                CreateAnchor(go);
            }
            
            //string json = JsonConvert.SerializeObject(_placedObjects);
            //
            //using (StreamWriter writer = File.CreateText(_filePathSavedObjs))
            //{
            //    writer.Write(json);
            //}
        }

        private void CreateAnchor(GameObject usedObj)
        {
            _workingAnchor = usedObj.AddComponent<OVRSpatialAnchor>();
            Debug.LogWarning("Create Anchor: " + _workingAnchor.gameObject.name);
            StartCoroutine(AnchorCreated(_workingAnchor));
        }

        private IEnumerator AnchorCreated(OVRSpatialAnchor anchor)
        {
            while (!anchor.Created && !anchor.Localized)
            {
                yield return new WaitForEndOfFrame();
            }
            
            _allRunningAnchors.Add(anchor);
            Debug.LogWarning("anchor.Save");
            anchor.Save((anch, success) =>
            {
                if (!success) return;
                _allSavedAnchors.Add(anch);
                //SaveUuidToPlayerPrefs(anch.Uuid, anch.gameObject);
                SaveAnchorUuidToExternalStore(anchor);
            });
        }
        
        void SaveUuidToPlayerPrefs(Guid uuid, GameObject saveObj)
        {
            // Write uuid of saved anchor to file
            if (!PlayerPrefs.HasKey(NumUuidsPlayerPref))
            {
                PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
            }

            int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
            PlayerPrefs.SetString("uuid" + playerNumUuids, uuid.ToString());

            if (saveObj.CompareTag("Wall"))
            {
                PlayerPrefs.SetInt("isWall" + playerNumUuids, 1);
                PlayerPrefs.SetFloat("ScalingX" + playerNumUuids, saveObj.transform.localScale.x);
                PlayerPrefs.SetFloat("ScalingY" + playerNumUuids, saveObj.transform.localScale.y);
                PlayerPrefs.SetFloat("ScalingZ" + playerNumUuids, saveObj.transform.localScale.z);
            }
            else
            {
                PlayerPrefs.SetInt("isWall" + playerNumUuids, 0);
                PlayerPrefs.SetFloat("ScalingX" + playerNumUuids, saveObj.transform.GetChild(0).localScale.x);
                PlayerPrefs.SetFloat("ScalingY" + playerNumUuids, saveObj.transform.GetChild(0).localScale.y);
                PlayerPrefs.SetFloat("ScalingZ" + playerNumUuids, saveObj.transform.GetChild(0).localScale.z);
            }
            PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);
            Debug.LogWarning(saveObj.name + playerNumUuids);
        }
        
        public void LoadAnchorsByUuid()
        {
            // Get number of saved anchor uuids
            if (!PlayerPrefs.HasKey(NumUuidsPlayerPref))
            {
                PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
            }

            var playerUuidCount = PlayerPrefs.GetInt(NumUuidsPlayerPref);
            Debug.Log($"Attempting to load {playerUuidCount} saved anchors.");
            if (playerUuidCount == 0)
                return;

            var uuids = new Guid[playerUuidCount];
            for (int i = 0; i < playerUuidCount; ++i)
            {
                var uuidKey = "uuid" + i;
                var currentUuid = PlayerPrefs.GetString(uuidKey);
                Debug.Log("QueryAnchorByUuid: " + currentUuid);

                uuids[i] = new Guid(currentUuid);
                _anchorSavedUUIDList[i] = uuids[i];
            }

            Load(new OVRSpatialAnchor.LoadOptions
            {
                Timeout = 0,
                StorageLocation = OVRSpace.StorageLocation.Local,
                Uuids = uuids
            });
        }
        
        private void Load(OVRSpatialAnchor.LoadOptions options) => OVRSpatialAnchor.LoadUnboundAnchors(options, anchors =>
        {
            if (anchors == null)
            {
                Debug.Log("Query failed.");
                return;
            }

            foreach (var anchor in anchors)
            {
                if (anchor.Localized)
                {
                    _onLoadAnchor(anchor, true);
                }
                else if (!anchor.Localizing)
                {
                    anchor.Localize(_onLoadAnchor);
                }
            }
        });
        
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

        private void OnLocalized(OVRSpatialAnchor.UnboundAnchor unboundAnchor, bool success)
        {
            var pose = unboundAnchor.Pose;
            //SPLacedObject tmp = new SPLacedObject();
            int isWall = 0;
            float scaleX = 0.0f;
            float scaleY = 0.0f;
            float scaleZ = 0.0f;

            foreach (var ID in _anchorSavedUUIDList)
            {
                if (unboundAnchor.Uuid == ID)
                {
                    isWall = PlayerPrefs.GetInt("isWall" + ID);
                    scaleX = PlayerPrefs.GetFloat("ScalingX" + ID);
                    scaleY = PlayerPrefs.GetFloat("ScalingY" + ID);
                    scaleZ = PlayerPrefs.GetFloat("ScalingZ" + ID);
                    break;
                }
            }

            var go = Instantiate(isWall > 0 ? _wallPrefab : _innerPrefab, pose.position, pose.rotation);

            go.transform.localScale = new Vector3(scaleX,scaleY,scaleZ);
            
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
