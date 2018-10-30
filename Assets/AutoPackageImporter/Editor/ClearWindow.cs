using UnityEditor;
using UnityEngine;

namespace AutoPackageImporter.Editor
{
    /// <summary>
    /// The confirmation dialog for deleting packages in the Window and macOS folders.
    /// </summary>
    /// <inheritdoc />
    internal class ClearWindow : EditorWindow
    {
        public string[] Files { private get; set; }

        private void OnGUI()
        {
            GUILayout.Label("\nAre you sure you want to delete the following files?\n\n" +
                            string.Join("\n", Files) + "\n");
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Yes"))
            {
                AutoPackageImporter.Clear(Files);
                Close();
            }

            if (GUILayout.Button("No")) Close();

            GUILayout.EndHorizontal();
            GUILayout.Space(20f);
        }
    }
}