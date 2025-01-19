using System;
using System.Collections.Generic;
using TankGame.App.Factory;
using TankGame.App.Object_Pool;
using TankGame.App.Projectiles;
using TankGame.Core.Services;
using TankGame.Core.Utils.Enums;

namespace TankGame.App.Infrastructure.Services.PoolsService
{
    public interface IPoolsService : IService
    {
        void RegisterPool<T>(string key) where T : IPoolableObject;
        ObjectPool<T> GetPool<T>() where T : IPoolableObject;
        Projectile GetProjectile(ProjectileTypeId projectileTypeId);
        void Dispose();
    }

    public class PoolsService : IPoolsService
    {
        private readonly IGameFactory _gameFactory;

        public PoolsService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private readonly Dictionary<Type, object> _pools = new();

        public void RegisterPool<T>(string key) where T : IPoolableObject
        {
            var type = typeof(T);

            if (_pools.ContainsKey(type))
                throw new InvalidOperationException($"Pool with key '{type}' is already registered.");

            var parent = _gameFactory.CreateEmptyObjectWithName(key);
            var pool = _gameFactory.CreatePool<T>(parent.transform, true);
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
    }

    public interface IPoolableObject
    {
        void OnSpawned();
        void ReturnToPool();
        void OnDespawned();
    }
}
