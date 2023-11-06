using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    
    private TilePool _tilePool;
    
    [SerializeField] private Vector3 _spawnPosition;
    public Vector3 SpawnPosition => _spawnPosition;
    public TileType tileType;
    

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("SpawnTile"))
        {
            GameObject tiles = TilePool._Instance.GetPooledObjects(TilePool._Instance.NextTileToSpawn);
            TilePool._Instance.ChangeNextTypeTileToPool(TileType.PostBridgeTile);
          //  TileType nextTileToGo = tiles.GetComponentInChildren<SpawnTile>().tileType == TileType.BridgeTile ? TileType.PostBridgeTile : TileType.NormalTile;
            TilePool._Instance.ChangeNextTypeTileToPool(TileType.NormalTile);
        }
    }
    
}
public enum TileType
{
    NormalTile,
    DoorTile,
    BridgeTile,
    PostBridgeTile
}
