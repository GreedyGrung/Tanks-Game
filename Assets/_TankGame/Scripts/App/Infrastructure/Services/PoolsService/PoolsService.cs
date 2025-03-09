using System;
using System.Collections.Generic;
using _TankGame.App.Factory;
using _TankGame.App.Infrastructure.Services.StaticData;
using _TankGame.App.Projectiles;
using _TankGame.App.Utils.Enums;

namespace _TankGame.App.Infrastructure.Services.PoolsService
{
    public class PoolsService : IPoolsService
    {
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;

        public PoolsService(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }

        private readonly Dictionary<Type, object> _pools = new();

        public void RegisterPool<T>() where T : IPoolableObject
        {
            var type = typeof(T);

            if (_pools.ContainsKey(type))
                throw new InvalidOperationException($"Pool with key '{type}' is already registered.");

            var poolConfig = _staticDataService.ForPool<T>();
            var parent = _gameFactory.CreateEmptyObjectWithName(poolConfig.PoolSceneName);
            var pool = _gameFactory.CreatePool<T>(parent.transform, poolConfig);
            _pools.Add(type, pool);
        }

        public ObjectPool<T> GetPool<T>() where T : IPoolableObject
        {
            var type = typeof(T);

            if (!_pools.TryGetValue(type, out var pool))
                throw new InvalidOperationException($"No pool found with key '{type}'.");

            return pool as ObjectPool<T> ?? throw new InvalidCastException($"Pool with key '{type}' is not of type {typeof(T)}.");
        }

        public Projectile GetProjectile(ProjectileTypeId projectileTypeId)
        {
            return projectileTypeId switch
            {
                ProjectileTypeId.AP => GetPool<ArmorPiercingProjectile>().Take(),
                ProjectileTypeId.HEX => GetPool<HighExplosiveProjectile>().Take(),
                _ => throw new InvalidOperationException($"Unknown projectile type: {projectileTypeId}")
            };
        }

        public void Dispose()
        {
            _pools.Clear();
        }

        public void ReturnToPool<T>(T poolableObject) where T : IPoolableObject
        {
            var type = typeof(T);

            if (type == typeof(ArmorPiercingProjectile))
            {
                GetPool<ArmorPiercingProjectile>().Return(poolableObject as ArmorPiercingProjectile);
            }
            else if (type == typeof(HighExplosiveProjectile))
            {
                GetPool<HighExplosiveProjectile>().Return(poolableObject as HighExplosiveProjectile);
            }
        }
    }
}
