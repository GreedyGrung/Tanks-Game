using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject CreateHud();
        GameObject CreateInput();
        GameObject CreatePlayer(Vector3 at);
        Enemy CreateEnemy(EnemyTypeId type, Transform parent);
        void CreateSpawner(EnemySpawnerData spawnerData, Player player);
        void CleanupProgressWatchers();
    }
}