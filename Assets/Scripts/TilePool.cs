using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public static TilePool _Instance;

    private List<GameObject> _pooledObjects = new List<GameObject>();
    private int _amountToPool = 9;

    [SerializeField] private GameObject _tilePrefab = default;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject obj = Instantiate(_tilePrefab, gameObject.transform.position, gameObject.transform.rotation);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }

       

       
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }

        return null;
    }
    
    public List<GameObject> GetPooledObjects()
    {
        return _pooledObjects;
    }
}