using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    public void SpawnObstacleNPowerUps(float rateSpawn, OBSTACLE_TYPE gameObjectToInstantiate)
    { 
        float mod = PlayerController.instance.Score % 25;
        bool isScoreMultipleOf = mod == 0;
        if (isScoreMultipleOf)
        {
            TilePool._Instance.ChangeNextTypeTileToPool(TileType.BridgeTile);
        }
        mod = PlayerController.instance.Score % rateSpawn;
        isScoreMultipleOf = mod == 0;
        if (isScoreMultipleOf)
        {
            SpawnerBalloon.instance.GetPooledObject(gameObjectToInstantiate);
        }
    }
}
