using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private const string EnemiesStaticDataPath = "Data/Enemies";
    private const string LevelsStaticDataPath = "Data/Levels";

    private Dictionary<EnemyTypeId, BaseEnemyStaticData> _enemies;
    private Dictionary<string, LevelStaticData> _levels;

    public void LoadEnemies()
    {
        _enemies = Resources
            .LoadAll<BaseEnemyStaticData>(EnemiesStaticDataPath)
            .ToDictionary(config => config.EnemyType, config => config);

        _levels = Resources
            .LoadAll<LevelStaticData>(LevelsStaticDataPath)
            .ToDictionary(config => config.LevelKey, config => config);
    }

    public BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId) =>
        _enemies.TryGetValue(enemyTypeId, out BaseEnemyStaticData staticData)
        ? staticData
        : null;

    public LevelStaticData ForLevel(string sceneKey) =>
        _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData
        : null;
}