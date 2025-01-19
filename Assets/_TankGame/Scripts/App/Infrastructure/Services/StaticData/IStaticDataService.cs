using TankGame.App.Entities.Enemies.Base.Data;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.StaticData;
using TankGame.Core.Services;
using TankGame.Core.Utils.Enums;

namespace TankGame.App.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
        LevelStaticData ForLevel(string sceneKey);
        UIPanelConfig ForUIPanel(UIPanelId victoryPanel);
        ProjectileStaticData ForProjectile(ProjectileTypeId projectileTypeId);
        void LoadEnemies();
        ObjectPoolStaticData ForPool<T>() where T : IPoolableObject;
    }
}