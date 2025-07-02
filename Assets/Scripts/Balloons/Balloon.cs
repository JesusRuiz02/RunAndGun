using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Random = UnityEngine.Random;

public class Balloon : MonoBehaviour
{
    [SerializeField] private List<Transform> BalloonExplosionTransform;
    [SerializeField] private BallonPopStrategy _balloonPopStrategy = default;
    [SerializeField] private float _speed = default;
    [SerializeField] private List<MovementStrategy> _movementStrategies = default;

    [SerializeField] private Transform _player = default;
    [SerializeField] private GameObject _particleExplosion;


    public Transform Player => _player;
    public float Speed => _speed;
    public List<Transform> BalloonExplosionTransforms => BalloonExplosionTransform;

    private IPop _popStrategy => _balloonPopStrategy;

    public void Pop()
    {
        _popStrategy?.PopBalloon(this);
    }

    public OBSTACLE_TYPE _Obstacle_Type;
    void Start()
    {
        _player = Camera.main.transform;
        _speed += PlayerController.instance.Score / 15 ;
    }

    private void Update()
    {
        if (transform.position.z <= -35)
        {
            gameObject.SetActive(false);
        }
        
        MoveBalloon();

    }

    public void MoveBalloon()
    {
        foreach (var movementStrategy in _movementStrategies)
        {
            movementStrategy.MoveBalloon(this);
        }
    }


    public void CreateParticula()
    {
       GameObject particle = Instantiate(_particleExplosion, transform.position, Quaternion.identity);
        Destroy(particle, 0.5f);
    }
    


    private void OnEnable()
    {
        if (_Obstacle_Type == OBSTACLE_TYPE.Door)
        {
            transform.position = new Vector3(0,0.4f,125);
            transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
        else if(_Obstacle_Type == OBSTACLE_TYPE.ExtraLifePowerUp)
        {
            transform.position = new Vector3(0,0.7f,125);
        }
    }
}
public enum OBSTACLE_TYPE
{
    Balloon,
    PowerUp,
    BalloonSpawner,
    HeavyBalloon,
    ExtraLifePowerUp,
    HealPowerUp,
    BalloonMobile,
    ShapeBalloon,
    Door
}