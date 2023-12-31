using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlacedObjects
{
    public class PlacedCube : APlacedObject
    {
        [SerializeField] private List<Transform> _aiSpawns;

        /// <summary>
        /// Returns a List of spawn points that are not obstructed by a collider
        /// </summary>
        /// <returns>List of Transforms</returns>
        public List<Transform> GetValidSpawnPoints()
        {
            List<Transform> valid = new List<Transform>();
            Collider[] hitColliders = new Collider[20];
            bool hasCollision = false;

            foreach (var spawn in _aiSpawns)
            {
                Physics.OverlapSphereNonAlloc(spawn.position, 0.0f, hitColliders);
                foreach (var coll in hitColliders)
                {
                    if (coll == null || !coll.isTrigger)
                        continue;
                    hasCollision = true;
                    break;
                }

                if (!hasCollision)
                    valid.Add(spawn);
                else
                    hasCollision = false;
            }
            
            return valid;
        }
    }
}
