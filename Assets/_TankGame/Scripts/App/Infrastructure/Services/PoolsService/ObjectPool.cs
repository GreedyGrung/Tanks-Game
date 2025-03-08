using System.Collections.Generic;
using System.Threading.Tasks;
using _TankGame.App.Factory;
using UnityEngine;

namespace _TankGame.App.Infrastructure.Services.PoolsService
{
    public class ObjectPool<T> where T : IPoolableObject
    {
        private readonly Transform _container;
        private readonly bool _autoExpand;
        private readonly IGameFactory _gameFactory;
        private readonly Stack<T> _pool;

        public ObjectPool(int size, Transform container, bool autoExpand, IGameFactory gameFactory)
        {
            _pool = new();
            _container = container;
            _autoExpand = autoExpand;
            _gameFactory = gameFactory;

            for (int i = 0; i < size; i++)
            {
#pragma warning disable CS4014
                CreateItem();
#pragma warning restore CS4014
            }
        }

        public T Take()
        {
            if (_pool.TryPop(out T item))
            {
                item.OnSpawned();

                if (_pool.Count == 0 && _autoExpand)
                {
#pragma warning disable CS4014
                    CreateItem();
#pragma warning restore CS4014
                }

                return item;
            }

            throw new System.Exception("The pool is empty!");
        }

        public void Return(T item)
        {
            item.OnDespawned();
            _pool.Push(item);
        }

        private async Task CreateItem(bool isActiveByDefault = false)
        {
            var item = await _gameFactory.CreatePoolableObject<T>(_container, isActiveByDefault);
            _pool.Push(item);
        }
    }
}
