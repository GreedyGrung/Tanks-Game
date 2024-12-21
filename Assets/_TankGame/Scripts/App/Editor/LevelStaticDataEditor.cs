using System.Linq;
using TankGame.Core.Utils.Enums.Generated;
using TankGame.Scripts.App.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame.Scripts.App.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var levelData = target as LevelStaticData;

            if (GUILayout.Button("Collect"))
            {
                var spawners = FindObjectsOfType<SpawnMarker>()
                    .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.EnemyType, x.transform.position, x.IsRandom))
                    .ToList();

                var initialPoint = GameObject.FindGameObjectWithTag(Tags.PlayerSpawnPoint.ToString());

                levelData.SetEnemySpawners_Editor(spawners);
                levelData.SetLevelKey_Editor(SceneManager.GetActiveScene().name);
                levelData.SetPlayerPosition_Editor(initialPoint.transform.position);
            }

            EditorUtility.SetDirty(target);
        }
    }
}