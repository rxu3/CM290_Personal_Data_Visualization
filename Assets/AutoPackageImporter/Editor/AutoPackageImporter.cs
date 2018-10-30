using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AutoPackageImporter.Editor
{
    /// <summary>
    /// Automatically imports new and updated packages in the Assets/AutoPackageImporter/Windows
    /// and Assets/AutoPackageImporter/masOS folders.
    /// </summary>
    /// <inheritdoc />
    internal class AutoPackageImporter : AssetPostprocessor
    {
        public const string EditorPath = AutoPackageImporterPath + "Editor/";

        // ReSharper disable once MemberCanBePrivate.Global
        // should be public because it is used for test packages
        public const string Menu = "Tools/Auto Package Importer/";

        private const string AutoPackageImporterPath = "Assets/AutoPackageImporter/",
            AutoPackageImporterCsPath = EditorPath + "AutoPackageImporter.cs",
            WindowsPath = AutoPackageImporterPath + "Windows/",
            MacPath = AutoPackageImporterPath + "macOS/",
            AssetsKey = "AutoPackageImporterAssets",
            PackageExt = "unitypackage";

        private static bool _reimportWhenReady;

        static AutoPackageImporter()
        {
            AssetDatabase.importPackageStarted += StartImporting;
            AssetDatabase.importPackageCompleted += CompleteImporting;
            AssetDatabase.importPackageCancelled += CompleteImporting;
            AssetDatabase.importPackageFailed += CompleteImporting;
            EditorApplication.update += Update;
        }

        /// <summary>
        /// Saves a list of all assets before importing a package from the Windows or macOS folder.
        /// It's used to determine which assets have been imported and uninstall the package later.
        /// </summary>
        private static void StartImporting(string packagename)
        {
            if (!IsAutoPackageName(packagename)) return;
            var assets = AssetDatabase.GetAllAssetPaths();
            var json = JsonUtility.ToJson(new StringsWrapper(assets));
            EditorPrefs.SetString(AssetsKey, json);
        }

        private static void CompleteImporting(string packagename, string errorMessage)
        {
            CompleteImporting(packagename);
        }
        
        /// <summary>
        /// Determines which assets have been imported and permanently saves their paths as a
        /// <see cref="ScriptableObject"/> in order to be able to uninstall the imported package
        /// later.
        /// </summary>
        /// <remarks>
        /// It may be safer to get paths using the tar utility.
        /// </remarks>
        private static void CompleteImporting(string packagename)
        {
            if (!IsAutoPackageName(packagename)) return;
            var json = EditorPrefs.GetString(AssetsKey, "{}");
            var assets = JsonUtility.FromJson<StringsWrapper>(json);
            var newAssets = AssetDatabase.GetAllAssetPaths().Except(assets.Items);
            AutoImportedAssets.Create(newAssets.ToList());
        }

        /// <summary>
        /// Gets a value indicating whether to automatically import a package.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> if <param name="packagename" /> is the name of one of the
        /// packages in the Windows or macOS folder./>
        /// </returns>
        private static bool IsAutoPackageName(string packagename)
        {
            var p = AssetDatabase.GetAllAssetPaths().Where(IsAutoPackage);
            return p.Any(s => System.IO.Path.GetFileName(s) == packagename + "." + PackageExt);
        }

        /// <summary>
        /// Gets a value indicating whether to automatically import a package.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> if <param name="packagePath" /> is the path to one of the
        /// packages in the Windows or macOS folder./>
        /// </returns>
        private static bool IsAutoPackage(string packagePath)
        {
            const string basePath =
#if UNITY_EDITOR_WIN
                WindowsPath;
#else
                MacPath;
#endif
            return IsAutoPackage(packagePath, basePath);
        }

        /// <summary>
        /// Gets a value indicating whether to automatically import a package.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> if <param name="packagePath" /> is the path to one of the
        /// packages in the <param name="directory" /> folder./>
        /// </returns>
        private static bool IsAutoPackage(string packagePath, string directory)
        {
            return packagePath.StartsWith(directory) && packagePath.EndsWith("." + PackageExt);
        }
        
        /// <summary>
        /// <see cref="JsonUtility"/> doesn't support arrays, so you need to wrap them up in a
        /// wrapper class.
        /// </summary>
        [Serializable]
        private class StringsWrapper
        {
            public string[] Items;

            public StringsWrapper(string[] items)
            {
                Items = items;
            }
        }

        /// <summary>
        /// Re-imports packages from the Windows and macOS folders if necessary and if the Editor
        /// is not currently updating.
        /// </summary>
        private static void Update()
        {
            if (!_reimportWhenReady || EditorApplication.isUpdating) return;
            Reimport();
        }

        /// <summary>
        /// Re-imports new and updated packages in the Windows and macOS folders.
        /// This method is called after importing all assets.
        /// </summary>
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (importedAssets.Any(s => IsAutoPackage(s) || s == AutoPackageImporterCsPath))
            {
                _reimportWhenReady = true;
            }
        }

        /// <summary>
        /// Uninstalls and then imports packages again.
        /// </summary>
        [MenuItem(Menu + "Reimport", false, 1)]
        private static void Reimport()
        {
            _reimportWhenReady = false;
            Unimport();
            foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
            {
                if (!IsAutoPackage(assetPath)) continue;
                AssetDatabase.ImportPackage(assetPath, false);
            }
        }

        /// <summary>
        /// Uninstalls imported packages.
        /// </summary>
        [MenuItem(Menu + "Unimport", false, 1)]
        private static void Unimport()
        {
            AutoImportedAssets.Delete();
        }

        [MenuItem(Menu + "Clear...", false, 1)]
        private static void ShowClearWindow()
        {
            var paths = AssetDatabase.GetAllAssetPaths()
                .Where(s => s.StartsWith(WindowsPath) || s.StartsWith(MacPath));
            var size = new Rect(0f, 0f, 450f, 150f);
            var window = EditorWindow.GetWindowWithRect<ClearWindow>(size, true, "Clear...");
            window.Files = paths.ToArray();
        }

        /// <summary>
        /// Uninstalls imported packages and deletes packages from the Windows and macOS folders.
        /// </summary>
        public static void Clear(IEnumerable<string> files)
        {
            Unimport();
            foreach (var f in files) AssetDatabase.DeleteAsset(f);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Packs the contents of the Windows and macOS folders and the importing scripts into a
        /// single package.
        /// </summary>
        [MenuItem(Menu + "Export...", false, 1)]
        private static void Export()
        {
            var assets = new List<string>
            {
                AutoPackageImporterCsPath,
                EditorPath + "AutoImportedAssets.cs",
                EditorPath + "ClearWindow.cs"
            };
            var allAssets = AssetDatabase.GetAllAssetPaths();
            var packages = allAssets.Where(s => IsAutoPackage(s, WindowsPath) ||
                                                IsAutoPackage(s, MacPath));
            assets.AddRange(packages);
            var filename = EditorUtility.SaveFilePanel("Export package...", string.Empty,
                string.Empty, PackageExt);
            if (string.IsNullOrEmpty(filename)) return;
            AssetDatabase.ExportPackage(assets.ToArray(), filename,
                ExportPackageOptions.Interactive);
        }
    }
}