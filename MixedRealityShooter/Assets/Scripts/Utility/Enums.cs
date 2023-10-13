using UnityEngine;

namespace Utility
{
    public enum EGameStates
    {
        PrepareMRScene,
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
}
