using System.Linq;
using System.Reflection;
using TankGame.App.Utils;
using UnityEditor;
using UnityEngine;

namespace TankGame.App.Editor
{
    [CustomPropertyDrawer(typeof(SceneNameSelectorAttribute))]
    public class SceneNameSelectorDrawer : PropertyDrawer
    {
        private static string[] _sceneNames;

        static SceneNameSelectorDrawer()
        {
            var fields = typeof(SceneNames)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
                .ToArray();

            _sceneNames = fields
                .Select(f => (string)f.GetRawConstantValue())
                .ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use [SceneNameSelector] with a string.");
                return;
            }

            var currentValue = property.stringValue;

            int index = System.Array.IndexOf(_sceneNames, currentValue);
            if (index < 0)
            {
                index = 0;
            }

            index = EditorGUI.Popup(position, label.text, index, _sceneNames);

            property.stringValue = _sceneNames[index];
        }
    }
}