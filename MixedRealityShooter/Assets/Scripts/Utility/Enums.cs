using UnityEngine;

namespace Utility
{
    public enum EGameStates
    {
        PrepareMRScene,
        PrepareVRScene,
        InHub,
        InGame,
        GameOver
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
}
