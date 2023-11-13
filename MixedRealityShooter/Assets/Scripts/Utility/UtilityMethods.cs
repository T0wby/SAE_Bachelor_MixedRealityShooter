using System;
using UnityEngine;

namespace Utility
{
    public class UtilityMethods : MonoBehaviour
    {
        public static void CalcBoxTransform(ref GameObject box, Vector3 startPos, Vector3 widthPos, Vector3 heightPos, Vector3 endPos)
        {
            if (box == null)return;
            Vector3 widthVector = widthPos - startPos;
            float height = Mathf.Abs(widthPos.y - heightPos.y);
            Vector3 scaleVector = endPos - heightPos;
            
            Vector3 boxSize = new Vector3(widthVector.magnitude, height, scaleVector.magnitude);
            
            // Set position
            box.transform.position = (startPos + endPos) * 0.5f;
            
            // Set rotation
            var rotation = Quaternion.LookRotation(widthVector.normalized, Vector3.up)* Quaternion.Euler(0.0f, 90.0f, 0.0f);
            box.transform.rotation = rotation;
            
            // Set scale
            box.transform.localScale = boxSize;
            
        }
        public static void CalcBoxTransform(ref GameObject box, ref GameObject parent, Vector3 startPos, Vector3 widthPos, Vector3 heightPos, Vector3 endPos)
        {
            if (box == null || parent == null)return;
            
            // scale
            box.transform.localScale = new Vector3(Vector3.Distance(startPos, widthPos),
                Vector3.Distance(widthPos, heightPos), Vector3.Distance(heightPos, endPos));
            
            // origin
            parent.transform.position = (startPos + endPos) * 0.5f;
            
            // rotation
            var rotation =
                Quaternion.LookRotation(((startPos + widthPos) * 0.5f) - parent.transform.position, Vector3.up);
            rotation.z = 0; 
            rotation.x = 0; 
            parent.transform.rotation = rotation;
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