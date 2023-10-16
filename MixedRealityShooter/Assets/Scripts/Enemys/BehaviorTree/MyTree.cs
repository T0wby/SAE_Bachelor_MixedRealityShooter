using UnityEngine;
using UnityEngine.AI;

namespace Enemys.BehaviorTree
{
    public abstract class MyTree : MonoBehaviour
    {
        private Node _root = null;
        protected NavMeshAgent _agent;
        protected int _enemyLayerMask;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            _root?.CalculateState();
        }

        protected abstract Node SetupTree();
    }
}
