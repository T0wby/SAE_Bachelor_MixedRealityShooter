using UnityEngine;
using Utility;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        #region Variables

        private EGameStates _currState = EGameStates.PrepareMRScene;

        #endregion

        #region Properties

        public EGameStates CurrState
        {
            get => _currState;
            set => _currState = value;
        }

        #endregion
    }
}
