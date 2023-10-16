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
        GameStart
    }

    public enum EWeaponType
    {
        AssaultRifle,
        Pistol,
        Dagger,
        Grenade
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
        NONE,
        Wall,
        Sphere,
        Cylinder
    }
    
    public enum ENodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
}
