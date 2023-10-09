using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        #region Variables

        private EGameStates _currState = EGameStates.PrepareMRScene;
        public UnityEvent OnGameStateChange;

        #endregion

        #region Properties

        public EGameStates CurrState
        {
            get
            {
                return _currState;
            }
            set
            {
                _currState = value;
                OnGameStateChange.Invoke();
            }
        }

        #endregion
    }
}
