using Assets.Scripts.Infrastructure;

public interface IStaticDataService : IService
{
    BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    LevelStaticData ForLevel(string sceneKey);
    void LoadEnemies();
}
