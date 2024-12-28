using System;
using System.Linq;
using TankGame.App.Utils;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace TankGame.App.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = target as UniqueId;

            if (string.IsNullOrEmpty(uniqueId.Id))
            {
                Generate(uniqueId);
            }
            else
            {
                UniqueId[] ids = FindObjectsOfType<UniqueId>();

                if (ids.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                {
                    Generate(uniqueId);
                }
            }
        }

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid()}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}
