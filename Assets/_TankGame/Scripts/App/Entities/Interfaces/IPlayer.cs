using _TankGame.App.Entities.Player;
using UnityEngine;

namespace _TankGame.App.Entities.Interfaces
{
    public interface IPlayer
    {
        IHealth Health { get; }
        PlayerWeapon Weapon { get; }
        Transform Transform { get; }
    }
}