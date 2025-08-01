using System.Threading.Tasks;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.StaticData.Enemies;
using TankGame.App.StaticData.Environment;
using TankGame.App.StaticData.UI;
using TankGame.App.Utils.Enums;

namespace TankGame.App.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
        LevelStaticData ForLevel(string sceneKey);
        UIPanelConfig ForUIPanel(UIPanelId victoryPanel);
        ProjectileStaticData ForProjectile(ProjectileTypeId projectileTypeId);
        Task LoadStaticData();
        ObjectPoolStaticData ForPool<T>() where T : IPoolableObject;
    }
}