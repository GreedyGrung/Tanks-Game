using System.Collections.Generic;
using TankGame.App.StaticData.Enemies;
using TankGame.Core.Editor;
using UnityEngine;

namespace TankGame.App.StaticData.Environment
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [SceneNameSelector]
        [SerializeField] private string _levelKey;
        [SerializeField] private Vector3 _playerPosition;
        [SerializeField] private List<EnemySpawnerData> _enemySpawners;

        public string LevelKey => _levelKey;
        public Vector3 PlayerPosition => _playerPosition;
        public IReadOnlyList<EnemySpawnerData> EnemySpawners => _enemySpawners;

        public void SetLevelKey_Editor(string key)
        {
            if (!Application.isEditor)
            {
                Debug.LogError("Method is only accessible from editor mode!");

                return;
            }

            _levelKey = key;
        }

        public void SetPlayerPosition_Editor(Vector3 playerPosition)
        {
            if (!Application.isEditor)
            {
                Debug.LogError("Method is only accessible from editor mode!");

                return;
            }

            _playerPosition = playerPosition;
        }

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
    }
}