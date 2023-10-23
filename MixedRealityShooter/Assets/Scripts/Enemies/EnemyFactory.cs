using System;
using System.Collections.Generic;
using Utility;

namespace Enemies
{
    public class EnemyFactory
    {
        private EnemyPool[] _enemyPools;
        
        public EnemyFactory(EnemyPool[] enemyPools)
        {
            _enemyPools = enemyPools;
        }

        public AEnemy CreateEnemy(EEnemyType type)
        {
            foreach (var pool in _enemyPools)
            {
                if (type == pool.Type)
                {
                    return pool.Pool.GetItem();
                }
            }

            return null;
        }
    }
}
