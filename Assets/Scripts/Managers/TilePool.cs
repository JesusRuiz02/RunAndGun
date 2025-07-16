using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class TilePool : MonoBehaviour
{
    public static TilePool _Instance;
    [SerializeField] private TileType _nextTileToSpawn;
    [SerializeField] private TileScriptableObjManager _tileScriptableObjManager = default;
    private String lastTileSpawned = default;
    public TileType NextTileToSpawn => _nextTileToSpawn;
    public bool isBridgeNextTile;
    private int _amountToPool = 9;
    [SerializeField] private GameObject _tilePrefab = default;
    private Dictionary<string, List<GameObject>> _tileIdToObjects = new();

    public Action<TileType> OntilePoolChange;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
    }

    public void OnChangeTile(TileType newTiletype)
    {
        _nextTileToSpawn = newTiletype;
        if (OntilePoolChange != null)
        {
            OntilePoolChange.Invoke(newTiletype);
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
        } while (nextTile == NextTileToSpawn);
       // SkyboxManager.GetInstance.SwitchSkybox((nextTile, 1));
        OnChangeTile(nextTile);

    }

    public GameObject GetNextTileToSet(List<GameObject> tiles)
    {
        if (isBridgeNextTile)
        {
            foreach (var objectTile in tiles)
            {
                if (objectTile.GetComponent<IsBridge>() != null)
                {
                    isBridgeNextTile = false;
                    return objectTile;
                }
            }
            
        }
     

        var nonBridgeTiles = tiles.FindAll(t => t.GetComponent<IsBridge>() == null);

        if (nonBridgeTiles.Count == 0)
            return null;

        int index = UnityEngine.Random.Range(0, nonBridgeTiles.Count);
        return nonBridgeTiles[index];
    }

    public GameObject GetPooledObjects(TileType type)
    {
        var tileList = _tileScriptableObjManager.GetTileListByType(type);
        GameObject _tileToSet = null;
        string tileToSetId= "";
       // Cycle for getting a tile that is not the last spawned tile
       int attempts = 0;
        do
        {
            _tileToSet = GetNextTileToSet(tileList);
            attempts++;
            tileToSetId = _tileToSet.GetComponent<TileIdentifier>().TileID;
        } while (tileToSetId == lastTileSpawned && attempts < 5);

        GameObject tileInPool = GetPooledObjectById(tileToSetId);
            if (tileInPool != null)
            {
                var spawnTile = tileInPool.GetComponentInChildren<SpawnTile>();
                tileInPool.transform.position = spawnTile.SpawnPosition;
                tileInPool.SetActive(true);
                lastTileSpawned = tileInPool.GetComponent<TileIdentifier>().TileID;
                return tileInPool;
            }
        // If no inactive object found, instantiate a new one
        var tiles = _tileToSet.GetComponentInChildren<SpawnTile>();
        GameObject currentTile = Instantiate(_tileToSet, tiles.SpawnPosition, tiles.transform.rotation);
        lastTileSpawned = currentTile.GetComponent<TileIdentifier>().TileID;
        AddTileToDictionary(currentTile, lastTileSpawned);
        return currentTile;
    }

    private void AddTileToDictionary(GameObject tile, string id)
    {
        if (!_tileIdToObjects.ContainsKey(id))
        { _tileIdToObjects[id] = new List<GameObject>(); }
        _tileIdToObjects[id].Add(tile);
    }

    void Start()
    {
        _tileScriptableObjManager.DivideTileByEnviroment();
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject obj = Instantiate(_tilePrefab, gameObject.transform.position, gameObject.transform.rotation);
            obj.SetActive(false);
            lastTileSpawned = obj.GetComponent<TileIdentifier>().TileID;

            var idComp = obj.GetComponent<TileIdentifier>();
            AddTileToDictionary(obj, idComp.TileID);
        }
    }

    public GameObject GetPooledObjectById(string tileId)
    {
        if (_tileIdToObjects.TryGetValue(tileId, out var tileList))
        {
            foreach (var obj in tileList)
            {
                if (!obj.activeInHierarchy)
                    return obj;
            }
        }
        return null; 
    }
}