using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    
   
    [SerializeField] private Vector3 _spawnPosition;
    public Vector3 SpawnPosition => _spawnPosition;
    public TileType tileType;
    [SerializeField] private bool _isBridge = false;
    public bool IsReturningBridge()
    {
        return _isBridge;
    }
  
    
    
    
}
