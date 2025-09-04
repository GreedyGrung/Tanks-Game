using Cysharp.Threading.Tasks;
using TankGame.Runtime.Infrastructure.Services.PoolsService;
using TankGame.Runtime.StaticData.Enemies;
using TankGame.Runtime.StaticData.Environment;
using TankGame.Runtime.StaticData.UI;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
        LevelStaticData ForLevel(string sceneKey);
        UIPanelConfig ForUIPanel(UIPanelId victoryPanel);
        ProjectileStaticData ForProjectile(ProjectileTypeId projectileTypeId);
        UniTask LoadStaticData();
        ObjectPoolStaticData ForPool<T>() where T : IPoolableObject;
    }
}