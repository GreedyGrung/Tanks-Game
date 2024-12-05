using Assets.Scripts.Infrastructure;

public interface IStaticDataService : IService
{
    BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    void LoadEnemies();
}