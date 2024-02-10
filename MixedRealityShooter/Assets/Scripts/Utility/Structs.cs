using System;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public struct SPLacedObject
    {
        public Guid UniqueId;
        public bool IsWall;
        public float ScalingX;
        public float ScalingY;
        public float ScalingZ;
    }
}