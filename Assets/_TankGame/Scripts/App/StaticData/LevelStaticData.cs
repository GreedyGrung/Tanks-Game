using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "levelData", menuName = "StaticData/Level")]
public class LevelStaticData : ScriptableObject
{
    [SerializeField] private string _levelKey;
    [SerializeField] private List<EnemySpawnerData> _enemySpawners;

    public string LevelKey => _levelKey;
    public IReadOnlyList<EnemySpawnerData> EnemySpawners => _enemySpawners;

    public void SetEnemySpawners_Editor(List<EnemySpawnerData> spawners)
    {
        if (!Application.isEditor)
        {
            Debug.LogError("Method is only accessible from editor mode!");

            return;
        }

        _enemySpawners.Clear();
        _enemySpawners = new List<EnemySpawnerData>(spawners);
    }

    public void SetLevelKey_Editor(string key)
    {
        if (!Application.isEditor)
        {
            Debug.LogError("Method is only accessible from editor mode!");

            return;
        }

        _levelKey = key;
    }
}