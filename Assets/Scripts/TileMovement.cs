using System;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] private Transform Tile = default;
    [SerializeField] private float _speed = default;
    public void Update()
    {
        Tile.Translate(0,0, -_speed * Time.deltaTime);

        if (Tile.position.z <= -15f)
        {
            gameObject.SetActive(false); 
            TilePool._Instance.GetPooledObjects(TilePool._Instance.NextTileToSpawn);
        }
        
    }
    
}

public enum TileType
{
    NormalTile,
    DesertTile,
    TrainTile,
    SnowTile,
}
