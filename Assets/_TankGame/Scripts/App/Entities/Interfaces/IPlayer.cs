using UnityEngine;

public interface IPlayer
{
    IHealth Health { get; }
    PlayerWeapon Weapon { get; }
    Transform Transform { get; }

    void Init(IInputService inputService, ISpawnersObserverService spawnersObserverService);
}