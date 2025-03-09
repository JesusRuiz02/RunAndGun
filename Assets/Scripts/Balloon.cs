using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Random = UnityEngine.Random;

public class Balloon : MonoBehaviour
{
    private int BalloonLife = 5;
    private Renderer _renderer;
    [SerializeField] private bool playerAttack;
    [SerializeField] private float _speed = default;
    [SerializeField] private Vector3 _newPosition = default;
    [SerializeField] private Transform _player = default;
    [SerializeField] private GameObject _particleExplosion;
    [SerializeField] private List<Transform> BalloonExplosionTransform;
    public OBSTACLE_TYPE _Obstacle_Type;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _player = Camera.main.transform;
        _speed += PlayerController.instance.Score / 5 ;
    }

    private void Update()
    {
        if (transform.position.z <= -35)
        {
            gameObject.SetActive(false);
        }
        
        if (_Obstacle_Type != OBSTACLE_TYPE.Door)
        {
            _newPosition = transform.position;
            _newPosition.y += Mathf.Sin(Time.time) * Time.deltaTime;
            transform.position = _newPosition;
            if (playerAttack)
            {
                transform.position = Vector3.MoveTowards(transform.position , _player.position, _speed * Time.deltaTime);  
            }
            else
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

    }

    public void MakeExplosionShape()
    {
        foreach (var explosionTransform in BalloonExplosionTransform)
        {
            GameObject balloon = SpawnerBalloon.instance.GetPooledObject(OBSTACLE_TYPE.BalloonMobile);
            balloon.transform.position = transform.position;
            balloon.transform.DOMove(explosionTransform.position, 0.4f).SetEase(Ease.Flash);
        }
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
           GameObject balloon = SpawnerBalloon.instance.GetPooledObject(OBSTACLE_TYPE.BalloonMobile);
           balloon.transform.position = transform.position;
           int random = Random.Range(0, 7);
           balloon.transform.DOMove(BalloonExplosionTransform[random].position, 0.4f).SetEase(Ease.Flash);
        }
    }

    public void BreakHeavyBalloon()
    {
        switch (BalloonLife)
        {
            case 5:
                _renderer.material.color = Color.black;
                gameObject.transform
                    .DOMove(new Vector3(transform.position.x, transform.position.y + -0.2f, transform.position.z), 0.5f).SetEase(Ease.Flash);
                BalloonLife--;
                break;
            case 4:
                _renderer.material.color = Color.green;
                gameObject.transform
                    .DOMove(new Vector3(transform.position.x, transform.position.y + -0.2f, transform.position.z), 0.5f).SetEase(Ease.Flash);
                BalloonLife--;
                break;
            case 3:
                _renderer.material.color = Color.yellow;
                gameObject.transform
                    .DOMove(new Vector3(transform.position.x, transform.position.y + -0.2f, transform.position.z), 0.5f).SetEase(Ease.Flash);
                BalloonLife--;
                break;
            case 2:
                _renderer.material.color = Color.red;
                gameObject.transform
                    .DOMove(new Vector3(transform.position.x, transform.position.y + -0.2f, transform.position.z), 0.5f).SetEase(Ease.Flash);
                BalloonLife--;
                break;
            case 1:
                _renderer.material.color = new Color(25, 25, 25);
                gameObject.transform
                    .DOMove(new Vector3(transform.position.x, transform.position.y + -0.2f, transform.position.z), 0.5f).SetEase(Ease.Flash);
                gameObject.SetActive(false);
                PlayerController.instance.AddScore(3);
                BalloonLife = 5;
                break;
        }
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