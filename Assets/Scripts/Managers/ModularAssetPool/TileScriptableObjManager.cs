using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "ScriptableObjects/TilePool/TileOrganizer")]
public class TileScriptableObjManager : ScriptableObject
{

    [SerializeField] private List<GameObject> generalTileList = new List<GameObject>();
    private List<TileList> categoryTileList = new List<TileList>();
    private Dictionary<TileType, List<GameObject>> tileMap = new();


    public void DivideTileByEnviroment()
    {

        //Dictionary with TileType as key and List of GameObjects as value
        tileMap.Clear();
        foreach (TileType tileType in Enum.GetValues(typeof(TileType)))
        {
            tileMap[tileType] = new List<GameObject>();
        }
        foreach (var tile in generalTileList)
        {
            var tileType = tile.GetComponentInChildren<SpawnTile>().tileType;
            if (tileMap.ContainsKey(tileType))
            {
                tileMap[tileType].Add(tile);
            }
        }
    }

    public List<GameObject> GetTileListByType(TileType type)
    {
       return tileMap.ContainsKey(type) ? tileMap[type] : new List<GameObject>();
    }


}

[Serializable]
public struct TileList
{
    public List<GameObject> tileList;
    public TileType tileType;
}
