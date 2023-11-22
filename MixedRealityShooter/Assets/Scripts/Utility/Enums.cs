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
        RoundOver
    }
    public enum EWeaponType
    {
        AssaultRifle,
        Pistol,
        Revolver,
        Dagger,
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
        Barrell = 2
    }
    public enum ENodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
}
