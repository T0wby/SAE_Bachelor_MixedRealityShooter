using UnityEngine;
using UnityEngine.AI;

namespace Enemies.BehaviorTree
{
    public abstract class MyTree : MonoBehaviour
    {
        private Node _root = null;
        [SerializeField] protected NavMeshAgent _agent;
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
