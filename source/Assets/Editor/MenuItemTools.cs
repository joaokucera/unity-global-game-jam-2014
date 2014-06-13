using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class MenuItemTools : MonoBehaviour
{
    [MenuItem("Tools / Create Folders")]
    public static void CreateFolders()
    {
        string applicationPath = Application.dataPath + "/";

        Directory.CreateDirectory(applicationPath + "Animations");
        Directory.CreateDirectory(applicationPath + "Fonts");
        Directory.CreateDirectory(applicationPath + "Materials");
        Directory.CreateDirectory(applicationPath + "Prefabs");
        Directory.CreateDirectory(applicationPath + "Models");
        Directory.CreateDirectory(applicationPath + "Resources");
        Directory.CreateDirectory(applicationPath + "Scenes");
        Directory.CreateDirectory(applicationPath + "Scripts");
        Directory.CreateDirectory(applicationPath + "Sounds");
        Directory.CreateDirectory(applicationPath + "Textures");

        AssetDatabase.Refresh();
    }

    [MenuItem("Tools / Create Prefabs")]
    public static void CreatePrefabs()
    {
        string localPath = Application.dataPath + "/Prefabs";
        if (!Directory.Exists(localPath))
        {
            Directory.CreateDirectory(localPath);
        }

        GameObject[] selectedObjects = Selection.gameObjects;
        foreach (GameObject item in selectedObjects)
        {
            string prefabPath = "Assets/Prefabs/" + item.name + ".prefab";
            CreateNewPrefab(prefabPath, item);
        }

        AssetDatabase.Refresh();
    }

    private static void CreateNewPrefab(string path, GameObject go)
    {
        PrefabUtility.CreatePrefab(path, go);
    }
}
