using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _TankGame.App.Infrastructure.Services.PoolsService;
using _TankGame.App.Projectiles;
using _TankGame.App.StaticData.Enemies;
using _TankGame.App.StaticData.Environment;
using _TankGame.App.StaticData.UI;
using _TankGame.App.Utils;
using _TankGame.App.Utils.Enums;
using UnityEngine.AddressableAssets;

namespace _TankGame.App.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, BaseEnemyStaticData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<UIPanelId, UIPanelConfig> _uiPanelsConfigs;
        private Dictionary<ProjectileTypeId, ProjectileStaticData> _projectiles;
        private Dictionary<ObjectPoolTypeId, ObjectPoolStaticData> _objectPools;

        public async Task LoadStaticData()
        {
            var enemyAssetsHandle = Addressables.LoadAssetsAsync<BaseEnemyStaticData>(Constants.EnemyDataLabel, null);
            await enemyAssetsHandle.Task;
            _enemies = enemyAssetsHandle.Result
                .ToDictionary(config => config.EnemyType, config => config);

            var levelsAssetsHandle = Addressables.LoadAssetsAsync<LevelStaticData>(Constants.LevelDataLabel, null);
            await levelsAssetsHandle.Task;
            _levels = levelsAssetsHandle.Result
                .ToDictionary(config => config.LevelKey, config => config);

            var uiAssetsHandle = Addressables.LoadAssetAsync<UIPanelsStaticData>(Constants.UiDataLabel);
            await uiAssetsHandle.Task;
            _uiPanelsConfigs = uiAssetsHandle.Result
                .Configs
                .ToDictionary(config => config.Id, config => config);

            var projectilesAssetsHandle = Addressables.LoadAssetsAsync<ProjectileStaticData>(Constants.ProjectileDataLabel, null);
            await projectilesAssetsHandle.Task;
            _projectiles = projectilesAssetsHandle.Result
                .ToDictionary(config => config.Id, config => config);

            var objectPoolsAssetsHandle = Addressables.LoadAssetsAsync<ObjectPoolStaticData>(Constants.ObjectPoolDataLabel, null);
            await objectPoolsAssetsHandle.Task;
            _objectPools = objectPoolsAssetsHandle.Result
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