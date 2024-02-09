using UnityEngine;

namespace Utility
{
    public enum EGameStates
    {
        PrepareMRSceneWall,
        PrepareMRSceneInner,
        PreparePlayScene,
        InHub,
        InGame,
        GameOver,
        GameStart,
        RoundOver,
        GameDone
    }
    public enum EWeaponType
    {
        AssaultRifle,
        Pistol,
        Revolver,
        BatSaw,
        Grenade
    }
    public enum EEnemyType
    {
        RangeTP,
        RangePistol,
        RangeWalk,
        Melee
    }
    public enum EColliderState
    {
        NONE,
        Position,
        Rotation,
        Scale
    }
    public enum EPlaceableItemType
    {
        NONE = 0,
        Wall = 1,
        Barrell = 2,
        Shroom = 3
    }
    public enum ENodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
}
