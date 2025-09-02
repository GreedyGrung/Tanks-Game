using TankGame.Runtime.Entities.Player;
using UnityEngine;

namespace TankGame.Runtime.Entities.Interfaces
{
    public interface IPlayer
    {
        IHealth Health { get; }
        PlayerWeapon Weapon { get; }
        Transform Transform { get; }
        bool Deactivated { get; }
        void Initalize();
    }
}