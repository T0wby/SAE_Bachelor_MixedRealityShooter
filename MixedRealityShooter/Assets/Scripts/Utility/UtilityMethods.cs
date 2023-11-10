using System;
using UnityEngine;

namespace Utility
{
    public class UtilityMethods : MonoBehaviour
    {
        public static void CalcBoxTransform(ref GameObject box, Vector3 startPos, float heightPos, Vector3 endPos)
        {
            if (box == null)return;
            
            var origin = (startPos + endPos) * 0.5f;
            var scale = new Vector3(Math.Abs(endPos.x - startPos.x), Math.Abs(heightPos - startPos.y),
                Math.Abs(endPos.z - startPos.z));

            box.transform.position = origin;
            box.transform.rotation = Quaternion.identity;
            box.transform.localScale = scale;
        }
    }
}