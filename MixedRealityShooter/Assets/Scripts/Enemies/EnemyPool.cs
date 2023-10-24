using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Enemies
{
    public class EnemyPool : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _poolSize = 10;
        [SerializeField] private EEnemyType _type = EEnemyType.RangeTP;
        private ObjectPool<AEnemy> _pool;

        #endregion

        #region Properties

        public ObjectPool<AEnemy> Pool => _pool;
        public EEnemyType Type => _type;

        #endregion
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _pool = new ObjectPool<AEnemy>(_enemyPrefab, _poolSize, transform);
        }
    }
}
