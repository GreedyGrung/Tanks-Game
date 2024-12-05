using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private const string StaticDataPath = "Data/Enemies";

    private Dictionary<EnemyTypeId, BaseEnemyStaticData> _enemies;

    public void LoadEnemies()
    {
        _enemies = Resources
            .LoadAll<BaseEnemyStaticData>(StaticDataPath)
            .ToDictionary(config => config.EnemyType, config => config);
    }

    public BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId) =>
        _enemies.TryGetValue(enemyTypeId, out BaseEnemyStaticData staticData)
        ? staticData
        : null;
}