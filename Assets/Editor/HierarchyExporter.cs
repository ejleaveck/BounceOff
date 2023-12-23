using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;

public class HierarchyExporter
{
    [MenuItem("EJ's Tools/Export Hierarchy to Text")]
    public static void ExportHierarchy()
    {
        StringBuilder stringBuilder = new StringBuilder();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        Array.Sort(allObjects, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));

        foreach (GameObject go in allObjects)
        {
            if (go.transform.parent == null) // Root objects only
            {
                TraverseHierarchy(go.transform, 0, stringBuilder);
            }
        }

        string date = DateTime.Now.ToString("yyyy_MM_dd");
        string defaultName = "Hierarchy_Export_" + date + ".txt";
        string filePath = EditorUtility.SaveFilePanel("Save Hierarchy", "", defaultName, "txt");

        if (!string.IsNullOrEmpty(filePath))
        {
            File.WriteAllText(filePath, stringBuilder.ToString());
            EditorUtility.DisplayDialog("Export Complete", "Hierarchy exported to " + filePath, "OK");
        }
    }

    private static void TraverseHierarchy(Transform parent, int depth, StringBuilder stringBuilder)
    {
        // Use a different character for indentation to match the requested format
        string indent = new string(' ', depth * 4); // 4 spaces per hierarchy level

        // Add a folder symbol to indicate a group
        string line = depth == 0 ? parent.name + "/" : indent + "├── " + parent.name;

        // Check if the object has any children, if not, it's a file, so add the file type symbol
        if (parent.childCount == 0)
        {
            line += "last";
        }

        stringBuilder.AppendLine(line);

        // Now deal with the children
        for (int i = 0; i < parent.childCount; ++i)
        {
            Transform child = parent.GetChild(i);

            // Only add the branching symbol to the last item if it has no siblings following it
            if (i == parent.childCount - 1)
            {
                stringBuilder.AppendLine(indent + "└── " + child.name );
            }
            else
            {
                TraverseHierarchy(child, depth + 1, stringBuilder);
            }
        }
    }
}
