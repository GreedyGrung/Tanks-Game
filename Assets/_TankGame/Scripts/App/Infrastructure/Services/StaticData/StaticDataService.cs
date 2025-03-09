using System.Collections.Generic;
using System.Linq;
using _TankGame.App.Infrastructure.Services.PoolsService;
using _TankGame.App.Projectiles;
using _TankGame.App.StaticData.Enemies;
using _TankGame.App.StaticData.Environment;
using _TankGame.App.StaticData.UI;
using _TankGame.App.Utils;
using _TankGame.App.Utils.Enums;
using UnityEngine;

namespace _TankGame.App.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, BaseEnemyStaticData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<UIPanelId, UIPanelConfig> _uiPanelsConfigs;
        private Dictionary<ProjectileTypeId, ProjectileStaticData> _projectiles;
        private Dictionary<ObjectPoolTypeId, ObjectPoolStaticData> _objectPools;

        public void LoadStaticData()
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

            _projectiles = Resources
                .LoadAll<ProjectileStaticData>(Constants.ProjectilesStaticDataPath)
                .ToDictionary(config => config.Id, config => config);

            _objectPools = Resources
                .LoadAll<ObjectPoolStaticData>(Constants.ObjectPoolsStaticDataPath)
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

        public ProjectileStaticData ForProjectile(ProjectileTypeId projectileTypeId) =>
            _projectiles.TryGetValue(projectileTypeId, out ProjectileStaticData config)
                ? config
                : null;

        public ObjectPoolStaticData ForPool<T>() where T : IPoolableObject
        {
            if (typeof(T) == typeof(ArmorPiercingProjectile))
            {
                return _objectPools[ObjectPoolTypeId.ProjectileAP];
            }
            else if (typeof(T) == typeof(HighExplosiveProjectile))
            {
                return _objectPools[ObjectPoolTypeId.ProjectileHEX];
            }
            else
            {
                return null;
            }
        }
    }
}