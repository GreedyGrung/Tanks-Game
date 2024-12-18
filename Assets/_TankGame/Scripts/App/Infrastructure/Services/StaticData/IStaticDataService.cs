using Assets.Scripts.Infrastructure;

public interface IStaticDataService : IService
{
    BaseEnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    LevelStaticData ForLevel(string sceneKey);
    UIPanelConfig ForUIPanel(UIPanelId victoryPanel);
    void LoadEnemies();
}
