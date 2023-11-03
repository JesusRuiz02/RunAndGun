using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public static TilePool _Instance;
    private TileType _nextTileToSpawn;
    public TileType NextTileToSpawn => _nextTileToSpawn;
    [SerializeField] private GameObject[] _tilesPrefab;
   [SerializeField] private List<GameObject> _pooledObjects = new List<GameObject>();
    public List<GameObject> PooledObjects => _pooledObjects;
    private int _amountToPool = 9;
    [SerializeField] private GameObject _tilePrefab = default;
    
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
    }

    public void ChangeNextTypeTileToPool(TileType type)
    {
        _nextTileToSpawn = type;
    }
    
    public GameObject GetPooledObjects(TileType type)
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                if (_pooledObjects[i].GetComponentInChildren<SpawnTile>().tileType == type )
                {
                    _pooledObjects[i].transform.position = _pooledObjects[i].GetComponentInChildren<SpawnTile>().SpawnPosition;
                    _pooledObjects[i].SetActive(true);
                    return _pooledObjects[i];
                }
            }
        }
        for (int i = 0; i < _tilesPrefab.Length; i++)
        {
            if (_tilesPrefab[i].GetComponentInChildren<SpawnTile>().tileType == type)
            {
                GameObject currentTile = Instantiate(_tilesPrefab[i], _tilesPrefab[i].GetComponentInChildren<SpawnTile>().SpawnPosition, _tilesPrefab[i].transform.rotation);
                _pooledObjects.Add(currentTile);
                return currentTile;
            }
        }
        return null;
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
    
    
}