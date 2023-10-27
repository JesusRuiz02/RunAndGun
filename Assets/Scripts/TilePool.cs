using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public static TilePool _Instance;

   [SerializeField] private List<GameObject> _pooledObjects = new List<GameObject>();
    public List<GameObject> PooledObjects => _pooledObjects;
    private int _amountToPool = 9;
    [SerializeField] private GameObject _bridgePrefab = default;
    [SerializeField] private GameObject _tilePrefab = default;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
    }

    public void BringObjectToFront(List<GameObject> list, int index)
    {
        GameObject objectToMove = list[index];
        list.RemoveAt(index);
        list.Insert(0,objectToMove);
    }
    
    public void GetObjectBack(List<GameObject> list, int index)
    {
        GameObject objectToMove = list[index];
        print(objectToMove);
        list.RemoveAt(index);
        list.Insert(list.Count,objectToMove);
    }

    void Start()
    {
        GameObject bridge = Instantiate(_bridgePrefab, new Vector3(0,0,80), Quaternion.Euler(0,180,0));
        _pooledObjects.Add(bridge);
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject obj = Instantiate(_tilePrefab, gameObject.transform.position, gameObject.transform.rotation);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
        bridge.SetActive(false);
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