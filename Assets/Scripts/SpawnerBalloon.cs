using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBalloon : MonoBehaviour
{
    public static SpawnerBalloon instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private GameObject _defaultBalloonPrefab;
    [SerializeField] private List<GameObject> _balloonPrefabPool = new List<GameObject>();
    [SerializeField] private float _ballonSpeedRate = default;
    private List<GameObject> _obstacleBalloons = new List<GameObject>();

    private int amounToPool = 2;

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
            GameObject obj = Instantiate(_balloonPrefabPool[0]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    private void Update()
    {
        _ballonSpeedRate -= Time.deltaTime;
        if (_ballonSpeedRate <= 0 )
        {
            int random = Random.Range(0, 3);
            var spawnBalloon = random >= 2 ? OBSTACLE_TYPE.Balloon : OBSTACLE_TYPE.BalloonMobile;
            GetPooledObject(spawnBalloon);
            _ballonSpeedRate = 2;
        }
    }

    public void AddObstacleBalloonsToPool()
    {
        if (_obstacleBalloons.Count == 0)        
            return;
        int itemIndex =  Random.Range(0, _obstacleBalloons.Count);
       _balloonPrefabPool.Add(_obstacleBalloons[itemIndex]);
        _obstacleBalloons.RemoveAt(itemIndex);
    }

    public void GetRandomObstacleBalloon()
    {
        int itemIndex = Random.Range(0, _balloonPrefabPool.Count);
        OBSTACLE_TYPE balloonType = _balloonPrefabPool[itemIndex].GetComponent<Balloon>()._Obstacle_Type;
        GetPooledObject(balloonType);
    }

    public GameObject GetPooledObject(OBSTACLE_TYPE _type)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                if (pooledObjects[i].GetComponent<Balloon>()._Obstacle_Type == _type )
                {
                     pooledObjects[i].transform.position = new Vector3(Random.Range(-8, 8), Random.Range(8, 5), 125);
                     pooledObjects[i].SetActive(true);
                     return pooledObjects[i];
                }
            }
        }

        for (int i = 0; i < _balloonPrefabPool.Count; i++)
        {
            if (_balloonPrefabPool[i].GetComponent<Balloon>()._Obstacle_Type == _type)
            {
                GameObject currentBullet = Instantiate(_balloonPrefabPool[i],
                    new Vector3(Random.Range(-8, 8), Random.Range(8, 5), 125), Quaternion.Euler(-90,0,0));
                pooledObjects.Add(currentBullet);
                return currentBullet;
            }
        }
        return null;
    }
    
    
   
}