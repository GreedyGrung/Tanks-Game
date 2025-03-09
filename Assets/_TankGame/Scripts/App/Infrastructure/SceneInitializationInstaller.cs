using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _TankGame.App.Infrastructure
{
    public class SceneInitializationInstaller : MonoInstaller
    {
        [SerializeField] private List<MonoBehaviour> _initializers;

        public override void InstallBindings()
        {
            foreach (MonoBehaviour initializer in _initializers)
            {
                Container.Bind(initializer.GetType()).FromInstance(initializer).AsSingle();
            }
        }
    }
}