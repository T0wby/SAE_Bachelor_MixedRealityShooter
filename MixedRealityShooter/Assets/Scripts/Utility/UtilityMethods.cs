using System;
using UnityEngine;

namespace Utility
{
    public class UtilityMethods : MonoBehaviour
    {
        public static void CalcBoxTransform(ref GameObject box, Vector3 startPos, Vector3 widthPos, float heightPos, Vector3 endPos)
        {
            if (box == null)return;
            
            var widthMiddle = (startPos + widthPos) * 0.5f;
            
            var origin = (widthMiddle + endPos) * 0.5f;
            var scale = new Vector3(Math.Abs(endPos.x - widthMiddle.x), Math.Abs(heightPos - widthMiddle.y),
                Math.Abs(endPos.z - widthMiddle.z));

            box.transform.position = origin;
            box.transform.rotation = Quaternion.identity;
            box.transform.localScale = scale;
        }
        public static void CalcQuadTransform(ref GameObject quad, Vector3 startPoint, Vector3 secondPoint, Vector3 heightPoint)
        {
            if (quad == null)return;
            
            var origin = (startPoint + heightPoint) * 0.5f;
            var direction = secondPoint - startPoint;
            var distance = Vector3.Distance(secondPoint, startPoint);
            var scale = new Vector2(Math.Abs(distance), Math.Abs(heightPoint.y - startPoint.y));
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            // Add 90 degrees to the calculated rotation
            rotation *= Quaternion.Euler(0, 90, 0);

            quad.transform.position = origin;
            quad.transform.rotation = rotation;
            quad.transform.localScale = new Vector3(scale.x, scale.y, quad.transform.localScale.z);
        }
        public static void CalcQuadTransform(ref GameObject quad, Vector3 startPoint, Vector3 secondPoint, Vector3 heightPoint, float extraScale)
        {
            if (quad == null)return;
            
            var origin = (startPoint + heightPoint) * 0.5f;
            var direction = secondPoint - startPoint;
            var distance = Vector3.Distance(secondPoint, startPoint);
            var scale = new Vector2(Math.Abs(distance * extraScale), Math.Abs((heightPoint.y - startPoint.y) * extraScale));
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            // Add 90 degrees to the calculated rotation
            rotation *= Quaternion.Euler(0, 90, 0);

            quad.transform.position = origin;
            quad.transform.rotation = rotation;
            quad.transform.localScale = new Vector3(scale.x, scale.y, quad.transform.localScale.z);
        }
    }
}