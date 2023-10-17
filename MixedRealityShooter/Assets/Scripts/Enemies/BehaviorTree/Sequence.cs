using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Enemies.BehaviorTree
{
    public class Sequence : Node
    {
        //And operator

        #region Constructors
        public Sequence() : base()
        {

        }

        public Sequence(List<Node> children) : base(children)
        {

        }
        #endregion

        public override ENodeState CalculateState()
        {
            bool childIsRunning = false;

            foreach (Node node in _children)
            {
                switch (node.CalculateState())
                {
                    case ENodeState.FAILURE:
                        _state = ENodeState.FAILURE;
                        return _state;
                    case ENodeState.RUNNING:
                        childIsRunning = true;
                        continue;
                    case ENodeState.SUCCESS:
                        continue;
                    default:
                        _state = ENodeState.SUCCESS;
                        return _state;
                }
            }

            return childIsRunning ? ENodeState.RUNNING : ENodeState.SUCCESS;
        }
    }
}
