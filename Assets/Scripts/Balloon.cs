using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float _speed = default;
    [SerializeField] private Vector3 _newPosition = default;
    [SerializeField] private Transform _player = default;
    [SerializeField] private GameObject _particleExplosion;
    [SerializeField] private List<Transform> BalloonExplosionTransform;
    public OBSTACLE_TYPE _Obstacle_Type;
    void Start()
    {
        _player = Camera.main.transform;
        _speed += PlayerController.instance.Score / 5 ;
    }

    private void Update()
    {
        _newPosition = transform.position;
        _newPosition.y += Mathf.Sin(Time.time) * Time.deltaTime;
        transform.position = _newPosition;
        transform.position = Vector3.MoveTowards(transform.position , _player.position, _speed * Time.deltaTime);
    }

    public void CreateParticula()
    {
       GameObject particle = Instantiate(_particleExplosion, transform.position, Quaternion.identity);
        Destroy(particle, 0.5f);
    }
    

    public void BalloonExplosion()
    {
        for (int i = 0; i < 2; i++)
        {
           GameObject balloon = SpawnerBalloon.instance.GetPooledObject(OBSTACLE_TYPE.Balloon);
           balloon.transform.position = transform.position;
           int random = Random.Range(0, 7);
           balloon.transform.DOMove(BalloonExplosionTransform[random].position, 0.4f).SetEase(Ease.Flash);
        }
    }
    
    public enum OBSTACLE_TYPE
    {
        Balloon,
        PowerUp,
        BalloonSpawner
    }
}
