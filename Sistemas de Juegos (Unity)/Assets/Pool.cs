using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] public GameObject _originalPrefab;
    [SerializeField] private List<GameObject> _usedPool;
    [SerializeField] private List<GameObject> _availablePool;

    public GameObject GetFromPool()
    {
        GameObject pooledObject;

        if (_availablePool.Count == 0)
        {
            // Instantiate
            pooledObject = GameObject.Instantiate(_originalPrefab);
        }

        else
        {
            // Get First From Pool List
            pooledObject = _availablePool[0];
        }

        _usedPool.Add(pooledObject);
        _availablePool.Remove(pooledObject);

        return pooledObject;
    }

    public void Recycle(GameObject recycle)
    {
        _usedPool.Remove(recycle);
        _availablePool.Add(recycle);
    }
}
