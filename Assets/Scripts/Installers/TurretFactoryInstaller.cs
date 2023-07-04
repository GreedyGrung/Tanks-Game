using UnityEngine;
using Zenject;

public class TurretFactoryInstaller : MonoInstaller
{
    [SerializeField] private TurretFactory _turretFactory;

    public override void InstallBindings()
    {
        Container.
            Bind<TurretFactory>().
            FromInstance(_turretFactory).
            AsSingle().
            NonLazy();
    }
}
