﻿using UnityEngine;
using GreedyLogger.Settings;
using UnityEditor;

namespace GreedyLogger.Editor
{
    [CustomEditor(typeof(LoggingSettings))]
    internal class GreedyLoggerSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Code"))
            {
                if (!((LoggingSettings)target).CanBeGenerated())
                {
                    Debug.LogError("Generation aborted due to inconsistent LogImportance values!");
                    return;
                }

                GreedyLoggerCodeGenerator.Generate();
            }

            if (GUILayout.Button("Reset Contexts Filter"))
            {
                ((LoggingSettings)target).ResetFilter();
            }

            if (GUILayout.Button("Reset To Default"))
            {
                ((LoggingSettings)target).RestoreToDefaults();
            }
        }
    }
}