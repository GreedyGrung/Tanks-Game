using _TankGame.App.Infrastructure.Services.PoolsService;
using _TankGame.App.StaticData.Enemies;
using _TankGame.App.StaticData.Environment;
using _TankGame.App.StaticData.UI;
using _TankGame.App.Utils.Enums;

namespace _TankGame.App.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
        LevelStaticData ForLevel(string sceneKey);
        UIPanelConfig ForUIPanel(UIPanelId victoryPanel);
        ProjectileStaticData ForProjectile(ProjectileTypeId projectileTypeId);
        void LoadStaticData();
        ObjectPoolStaticData ForPool<T>() where T : IPoolableObject;
    }
}