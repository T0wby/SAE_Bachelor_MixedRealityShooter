using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Enemys.BehaviorTree
{
    public class Node
    {
        protected ENodeState _state;
        protected List<Node> _children = new List<Node>();

        private Dictionary<string, object> _data = new Dictionary<string, object>();
        private Node _parent;
        public Node Parent { get => _parent; set => _parent = value; }

        #region Constructors
        public Node()
        {
            _parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                Add(child);
        }
        #endregion

        #region Functions

        /// <summary>
        /// Adds a Node to the List of children from the current Node
        /// </summary>
        /// <param name="node">Node that will be added</param>
        private void Add(Node node)
        {
            node.Parent = this;
            _children.Add(node);
        }

        /// <summary>
        /// Used to calculate the current Node state
        /// </summary>
        /// <returns>Returns current Node state</returns>
        public virtual ENodeState CalculateState()
        {
            return ENodeState.FAILURE;
        }

        /// <summary>
        /// Sets the value of a key in the data of the node
        /// </summary>
        /// <param name="key">Key that will be changed</param>
        /// <param name="value">Value that will be added</param>
        public void SetData(string key, object value)
        {
            _data[key] = value;
        }

        /// <summary>
        /// Trys to get data from the Node or its parents
        /// </summary>
        /// <param name="key">Key that is gonna be searched</param>
        /// <returns>Returns the searched value if it was found</returns>
        public object GetData(string key)
        {
            if (_data.TryGetValue(key, out var value))
                return value;

            Node tmp = _parent;
            while (tmp != null)
            {
                value = tmp.GetData(key);
                if (value != null)
                    return value;
                tmp = tmp.Parent;
            }
            return null;
        }

        /// <summary>
        /// Deletes data from the Node or its parents
        /// </summary>
        /// <param name="key">Data key which will be removed</param>
        /// <returns>Returns if it was a success or not</returns>
        public bool DeleteData(string key)
        {
            if (_data.ContainsKey(key))
            {
                _data.Remove(key);
                return true;
            }

            Node tmp = _parent;
            while (tmp != null)
            {
                bool cleared = tmp.DeleteData(key);
                if (cleared)
                    return true;
                tmp = tmp.Parent;
            }
            return false;
        }

        /// <summary>
        /// Finds the root Node of the tree
        /// </summary>
        /// <param name="node">Node we are searching from</param>
        /// <returns>Returns the root Node reference</returns>
        public Node GetRoot(Node node)
        {
            if (node.Parent is null)
                return node;

            return GetRoot(node.Parent);
        }

        #endregion
    }
}
