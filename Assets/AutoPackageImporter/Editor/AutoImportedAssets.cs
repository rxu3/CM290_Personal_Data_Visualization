using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AutoPackageImporter.Editor
{
    /// <summary>
    /// Stores a list of imported assets to be able to delete them.
    /// </summary>
    /// <inheritdoc />
    internal class AutoImportedAssets : ScriptableObject
    {
        private const string Path = AutoPackageImporter.EditorPath + "ImportedAssets.asset";

        // ReSharper disable once NotAccessedField.Global
        public List<string> ImportedAssets = new List<string>();

        public static void Create(List<string> importedAssets)
        {
            var so = AssetDatabase.LoadAssetAtPath<AutoImportedAssets>(Path);
            if (so == null)
            {
                so = CreateInstance<AutoImportedAssets>();
                AssetDatabase.CreateAsset(so, Path);
            }

            importedAssets.Remove(Path);
            so.ImportedAssets = importedAssets;
            EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssets();
        }

        public static void Delete()
        {
            var so = AssetDatabase.LoadAssetAtPath<AutoImportedAssets>(Path);
            if (so != null) DeleteImportedAssets(so.ImportedAssets);
            AssetDatabase.DeleteAsset(Path);
            AssetDatabase.SaveAssets();
        }

        private static void DeleteImportedAssets(IEnumerable<string> importedAssets)
        {
            var directories = new List<string>();
            foreach (var importedAsset in importedAssets)
            {
                if (string.IsNullOrEmpty(importedAsset)) continue;
                if (Directory.Exists(importedAsset))
                {
                    directories.Add(importedAsset);
                    continue;
                }
                
                AssetDatabase.DeleteAsset(importedAsset);
            }

            foreach (var directory in directories)
            {
                var isNotEmpty = Directory.Exists(directory) &&
                                 Directory.GetFileSystemEntries(directory).Any();
                if (isNotEmpty) continue;
                AssetDatabase.DeleteAsset(directory);
            }
        }
    }
}