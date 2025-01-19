using TankGame.App.Entities.Player;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.Infrastructure.Services.SpawnersObserver;
using TankGame.Core.Services.Input;
using UnityEngine;

namespace TankGame.App.Entities.Interfaces
{
    public interface IPlayer
    {
        IHealth Health { get; }
        PlayerWeapon Weapon { get; }
        Transform Transform { get; }

        void Init(IInputService inputService, ISpawnersObserverService spawnersObserverService, IPoolsService poolsService);
    }
}