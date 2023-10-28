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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    public void SpawnObstacleNPowerUps(float rateSpawn, OBSTACLE_TYPE gameObjectToInstantiate)
    { 
        float mod = PlayerController.instance.Score % 20;
        bool isScoreMultipleOf = mod == 0 ? true : false;
        if (isScoreMultipleOf)
        {
            TilePool._Instance.BringObjectToFront(TilePool._Instance.PooledObjects,TilePool._Instance.PooledObjects.Count-1);
        }
        mod = PlayerController.instance.Score % rateSpawn;
        isScoreMultipleOf = mod == 0 ? true : false;
        if (isScoreMultipleOf)
        {
            SpawnerBalloon.instance.GetPooledObject(gameObjectToInstantiate);
        }
    }
}
