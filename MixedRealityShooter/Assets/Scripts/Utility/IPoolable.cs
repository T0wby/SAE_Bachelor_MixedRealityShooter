using UnityEngine;

namespace Utility
{
    public interface IPoolable<T> where T : MonoBehaviour, IPoolable<T>
    {
        void Initialize(ObjectPool<T> pool);
        void Reset();
        void Deactivate();
    }
}
