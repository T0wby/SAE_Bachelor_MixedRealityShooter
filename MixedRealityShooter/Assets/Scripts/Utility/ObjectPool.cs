using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        public ObjectPool(GameObject prefab, int size, Transform parent)
        {
            this._prefab = prefab;
            InitPool(size, parent);
        }

        private Transform _parent;
        private readonly GameObject _prefab;

        private Queue<T> _queue = new Queue<T>();
        
        public T GetItem()
        {
            T tmp;

            if (_queue.Count == 0)
            {
                tmp = GameObject.Instantiate(_prefab).GetComponent<T>();
                tmp.Initialize(this);
                return tmp;
            }

            tmp = _queue.Dequeue();
            tmp.Reset();

            return tmp;
        }

        public void ReturnItem(T item)
        {
            item.Deactivate();
            _queue.Enqueue(item);
        }

        private void InitPool(int size, Transform parent)
        {
            for (int i = 0; i < size; i++)
            {
                T tmp = GameObject.Instantiate(_prefab, parent).GetComponent<T>();
                tmp.Initialize(this);
                ReturnItem(tmp);
            }
        }
    }
}
