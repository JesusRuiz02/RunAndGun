using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TilePool : MonoBehaviour
{
    public static TilePool _Instance;
    [SerializeField] private TileType _nextTileToSpawn;
    public TileType NextTileToSpawn => _nextTileToSpawn;
    private bool isBridgeNextTile;
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
    

    public void ChangeBridgeTile()
    {
        isBridgeNextTile = true;
    }

    public void ChangeNextTypeTileToPool()
    {
        TileType nextTile = default;
        do
        {
            Random random = new Random();
            var values = Enum.GetValues(typeof(TileType));
            nextTile = (TileType)values.GetValue(random.Next(values.Length));
        }while(nextTile == NextTileToSpawn);
        SkyboxManager.GetInstance.SwitchSkybox((nextTile,1));
        _nextTileToSpawn = nextTile;
    }
    
    
    
    
    public GameObject GetPooledObjects(TileType type)
    {

        foreach (var objectTile in _pooledObjects)
        {
            if (!objectTile.activeInHierarchy)
            {
                var tile = objectTile.GetComponentInChildren<SpawnTile>();
                if (tile.tileType == type)
                {
                    if (isBridgeNextTile)
                    {
                        if (!tile.IsReturningBridge())
                        {
                            continue;
                        }
                        isBridgeNextTile = false;
                    } 
                    objectTile.transform.position = tile.SpawnPosition;
                    objectTile.SetActive(true);
                    return objectTile;
                }
                
            }
        }
        
        for (int i = 0; i < _tilesPrefab.Length; i++)
        {
            SpawnTile tile = _tilesPrefab[i].GetComponentInChildren<SpawnTile>();
            if (tile.tileType == type)
            {
                if (isBridgeNextTile)
                {
                    if (!tile.IsReturningBridge())
                    {
                        continue;
                    }
                    isBridgeNextTile = false;
                } 
                GameObject currentTile = Instantiate(_tilesPrefab[i], tile.SpawnPosition, _tilesPrefab[i].transform.rotation);
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