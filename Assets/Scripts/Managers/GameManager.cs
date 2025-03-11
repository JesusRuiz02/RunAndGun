using System;
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
        if ( PlayerController.instance.GetScoreToReset() >= 100)
        {
            Debug.Log("Score Reset");
            PlayerController.instance.StartResetScore();
            TilePool._Instance.ChangeNextTypeTileToPool();
        }
        float mod = PlayerController.instance.Score % 75;
        
        bool isScoreMultipleOf = mod == 0;
        if (isScoreMultipleOf)
        {
            TilePool._Instance.ChangeBridgeTile();
        }
        
        mod = PlayerController.instance.Score % rateSpawn;
        isScoreMultipleOf = mod == 0;
        if (isScoreMultipleOf)
        {
            SpawnerBalloon.instance.GetPooledObject(gameObjectToInstantiate);
        }
    }
}
