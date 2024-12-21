using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

namespace TankGame.Core.Editor
{
    public static class TagsAndLayersEnumsGenerator
    {
        private const string TagsOutputPath = "Scripts/Core/Utils/Enums/Generated/Tags.generated.cs";
        private const string LayersOutputPath = "Scripts/Core/Utils/Enums/Generated/Layers.generated.cs";

        [MenuItem("Tools/Generate Tags and Layers enums")]
        public static void Generate()
        {
            GenerateTagsEnum();
            GenerateLayersEnum();

            AssetDatabase.Refresh();

            Debug.Log("Tags & Layers enums generated successfully!");
        }

        private static void GenerateTagsEnum()
        {
            string[] tags = InternalEditorUtility.tags;

            var fileContent =
                "// This file is auto-generated. Any changes will be overwritten.\n" +
                "namespace TankGame.Core.Utils.Enums.Generated\n" +
                "{\n" +
                "    public enum Tags\n" +
                "    {\n";

            for (int i = 0; i < tags.Length; i++)
            {
                string safeName = tags[i].Replace(" ", "_");
                fileContent += $"        {safeName} = {i},\n";
            }

            fileContent +=
                "    }\n" +
                "}\n";

            string fullPath = Path.Combine(Constants.ProjectRootPath, TagsOutputPath);

            var directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(fullPath, fileContent);
        }

        private static void GenerateLayersEnum()
        {
            string[] layers = InternalEditorUtility.layers;

            var fileContent =
                "// This file is auto-generated. Any changes will be overwritten.\n" +
                "namespace TankGame.Core.Utils.Enums.Generated\n" +
                "{\n" +
                "    public enum Layers\n" +
                "    {\n";

            for (int i = 0; i < layers.Length; i++)
            {
                string safeName = layers[i].Replace(" ", "_");
                int layerNumber = LayerMask.NameToLayer(layers[i]);
                fileContent += $"        {safeName} = {layerNumber},\n";
            }

            fileContent +=
                "    }\n" +
                "}\n";

            string fullPath = Path.Combine(Constants.ProjectRootPath, LayersOutputPath);

            var directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(fullPath, fileContent);
        }
    }
}