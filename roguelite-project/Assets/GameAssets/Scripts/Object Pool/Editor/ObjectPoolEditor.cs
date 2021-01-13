using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectPool))]
public class ObjectPoolEditor : Editor
{
    private readonly int poolQuantity = 10;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Label((""));
        var pool = (ObjectPool)target;
        if(GUILayout.Button("Rebuild pool"))
        {
            pool.CreatePoolables(NewPoolable(pool.BaseObject, pool.transform));
        }
        if(GUILayout.Button("Reset pool"))
        {
            pool.CreatePoolables(ResetPool(pool.transform));
        }
    }

    private List<PoolableObject> NewPoolable(PoolableObject basePoolable, Transform parent)
    {
        var list = new List<PoolableObject>();
        for (int i = 0; i < poolQuantity; i++)
        {
            var obj = (PoolableObject)PrefabUtility.InstantiatePrefab(basePoolable, parent);
            list.Add(obj);
            obj.gameObject.SetActive(false);
        }
        EditorUtility.SetDirty(parent);
        return list;
    }

    private List<PoolableObject> ResetPool(Transform parent)
    {
        EditorUtility.SetDirty(parent);
        return new List<PoolableObject>();
    }
}