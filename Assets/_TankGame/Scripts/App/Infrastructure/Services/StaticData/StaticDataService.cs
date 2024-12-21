using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private Dictionary<EnemyTypeId, BaseEnemyStaticData> _enemies;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<UIPanelId, UIPanelConfig> _uiPanelsConfigs;

    public void LoadEnemies()
    {
        _enemies = Resources
            .LoadAll<BaseEnemyStaticData>(Constants.EnemiesStaticDataPath)
            .ToDictionary(config => config.EnemyType, config => config);

        _levels = Resources
            .LoadAll<LevelStaticData>(Constants.LevelsStaticDataPath)
            .ToDictionary(config => config.LevelKey, config => config);

        _uiPanelsConfigs = Resources
            .Load<UIPanelsStaticData>(Constants.UIPanelsStaticDataPath)
            .Configs
            .ToDictionary(config => config.Id, config => config);
    }

    public BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId) =>
        _enemies.TryGetValue(enemyTypeId, out BaseEnemyStaticData staticData)
        ? staticData
        : null;

    public LevelStaticData ForLevel(string sceneKey) =>
        _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData
        : null;

    public UIPanelConfig ForUIPanel(UIPanelId id) =>
        _uiPanelsConfigs.TryGetValue(id, out UIPanelConfig config)
        ? config
        : null;
}