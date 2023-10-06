using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    
    private TilePool _tilePool;

    [SerializeField] private Transform _tilePosition = default;
    [SerializeField] private Vector3 _SpawnPosition;

    void Start()
    {
        _tilePool = TilePool._Instance;

    }
    
    void LateUpdate()
    {
        _tilePool.GetPooledObject();
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject tiles = TilePool._Instance.GetPooledObject();

        if (tiles != null)
        {
           // var newPosition = tiles.transform.Find("NextTilePos");
            tiles.transform.position = _SpawnPosition;
            tiles.SetActive(true);
        }
            
        }
    }

        
}
