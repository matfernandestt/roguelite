﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public PoolableObject BaseObject;
    public List<PoolableObject> poolableObjects = new List<PoolableObject>();

    private int poolQuantity;

    public PoolableObject RequestObject(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        PoolableObject newObject;
        if (poolableObjects.Count > 0)
        {
            newObject = poolableObjects[0];
            poolableObjects.RemoveAt(0);
        }
        else
        {
            newObject = Instantiate(BaseObject);
        }
        newObject.transform.position = position;
        newObject.transform.rotation = rotation;
        newObject.transform.SetParent(parent);
        newObject.ActivateObject();
        return newObject;
    }

    public void ReturnObject(PoolableObject returnedObject, float timeToReturn = 0)
    {
        StartCoroutine(RestoringObject(returnedObject, timeToReturn));
    }

    private IEnumerator RestoringObject(PoolableObject returnedObject, float timeToReturn)
    {
        yield return new WaitForSeconds(timeToReturn);
        returnedObject.transform.SetParent(transform);
        returnedObject.transform.localPosition = Vector3.zero;
        poolableObjects.Add(returnedObject);
        returnedObject.DeactivateObject();
    }

    [ContextMenu("Create poolables")]
    public void CreatePoolables(List<PoolableObject> newPoolables)
    {
        foreach (var poolable in poolableObjects)
            DestroyImmediate(poolable.gameObject);
        poolableObjects.Clear();
        poolableObjects = newPoolables;
    }

    [ContextMenu("Setup children")]
    public void SetAllChildren()
    {
        poolableObjects.Clear();
        var children = gameObject.GetComponentsInChildren<PoolableObject>();
        foreach (var child in children)
        {
            poolableObjects.Add(child);
            child.gameObject.SetActive(false);
        }
    }
}
