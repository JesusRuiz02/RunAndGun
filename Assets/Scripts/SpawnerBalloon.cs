using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBalloon : MonoBehaviour
{
    public static SpawnerBalloon instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private GameObject[] _balloonPrefab;
    [SerializeField] private float _ballonSpeedRate = default;

    private int amounToPool = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amounToPool; i++)
        {
            GameObject obj = Instantiate(_balloonPrefab[0]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    private void Update()
    {
        _ballonSpeedRate -= Time.deltaTime;
        if (_ballonSpeedRate <= 0 )
        {
            GetPooledObject(Balloon.OBSTACLE_TYPE.Balloon);
            _ballonSpeedRate = 3;
        }
    }

    public GameObject GetPooledObject(Balloon.OBSTACLE_TYPE _type)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                if (pooledObjects[i].GetComponent<Balloon>()._Obstacle_Type == _type )
                {
                     pooledObjects[i].transform.position = new Vector3(Random.Range(-20, 20), Random.Range(20, 5), 125);
                     pooledObjects[i].SetActive(true);
                     return pooledObjects[i];
                }
            }
        }

        for (int i = 0; i < _balloonPrefab.Length; i++)
        {
            if (_balloonPrefab[i].GetComponent<Balloon>()._Obstacle_Type == _type)
            {
                GameObject currentBullet = Instantiate(_balloonPrefab[i],
                    new Vector3(Random.Range(-20, 20), Random.Range(20, 5), 125), Quaternion.Euler(-90,0,0));
                pooledObjects.Add(currentBullet);
                return currentBullet;
            }
        }
        return null;
    }

  
    

   /* public GameObject CreateBalloons(Balloon.OBSTACLE_TYPE _type)
    {
        GameObject obj = Instantiate(_balloonPrefab);
        pooledObjects.Add(obj);
        obj.SetActive(false);
        return obj;
    }*/
    
   
}