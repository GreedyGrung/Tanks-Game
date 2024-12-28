using UnityEditor;
using UnityEngine;

namespace TankGame.Core.Editor
{
    public class TagLayerChangePostprocessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            foreach (var assetPath in importedAssets)
            {
                if (assetPath.EndsWith("TagManager.asset"))
                {
                    Debug.LogWarning("Tags or Layers were changed! Don't forget to regenerate the enums! Use [Tools] -> [Generate Tags and Layers enums]");
                }
            }
        }
    }
}
