using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public GameObject ObjectPrefab;
    private Stack<GameObject> _objectsInPool = new Stack<GameObject>();

    public GameObject GetGameObject()
    {
        if (_objectsInPool.Count > 0)
        {
            return _objectsInPool.Pop();
        }
        return CreateNewGameObject();
    }
    
    public GameObject CreateNewGameObject()
    {
        GameObject clone = GameObject.Instantiate(ObjectPrefab);
        clone.transform.name = ObjectPrefab.name;
        if(!clone.TryGetComponent(out ReturnGameObjectToPool component))
        {
            component = clone.AddComponent<ReturnGameObjectToPool>();
            component.ObjectPool = this;
            // clone.GetComponent<ReturnGameObjectToPool>().ObjectPool = this;
        }
        return clone;
    }

    public void ReturnGameObjectToPool(GameObject go)
    {
        _objectsInPool.Push(go);
    }
}
