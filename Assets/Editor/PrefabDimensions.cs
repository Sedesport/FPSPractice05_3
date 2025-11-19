using UnityEngine;
using UnityEditor;

public class PrefabDimensions
{
    [MenuItem("Tools/Check Prefab Dimensions")]
    static void CheckDimensions()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            MeshRenderer mr = prefab.GetComponentInChildren<MeshRenderer>();
            if (mr != null)
            {
                Vector3 size = mr.bounds.size;
                Debug.Log($"{prefab.name} é¿ê°ñ@: {size.x:F2}m Å~ {size.y:F2}m Å~ {size.z:F2}m");
            }
        }
    }
}

