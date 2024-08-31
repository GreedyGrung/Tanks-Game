using UnityEngine;
using Zenject;

public class TankFactoryInstaller : MonoInstaller
{
    [SerializeField] private TankFactory _tankFactory;

    public override void InstallBindings()
    {
        Container.
            Bind<TankFactory>().
            FromInstance(_tankFactory).
            AsSingle().
            NonLazy();
    }
}
