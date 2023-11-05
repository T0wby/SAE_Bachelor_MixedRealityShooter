using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Enemies.BehaviorTree
{
    public class Selector : Node
    {
        //Or operator

        #region Constructors
        public Selector() : base() {}

        public Selector(List<Node> children) : base(children) {}
        #endregion

        public override ENodeState CalculateState()
        {
            foreach (Node node in _children)
            {
                switch (node.CalculateState())
                {
                    case ENodeState.FAILURE:
                        continue;
                    case ENodeState.RUNNING:
                        _state = ENodeState.RUNNING;
                        return _state;
                    case ENodeState.SUCCESS:
                        _state = ENodeState.SUCCESS;
                        return _state;
                    default:
                        continue;
                }
            }

            return ENodeState.FAILURE;
        }
    }
}
