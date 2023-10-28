using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour
{
    private int BalloonLife = 5;
    private Renderer _renderer;
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

    public void BreakHeavyBalloon()
    {
        switch (BalloonLife)
        {
            case 5:
                _renderer.material.color = Color.black;
                BalloonLife--;
                break;
            case 4:
                _renderer.material.color = Color.green;
                BalloonLife--;
                break;
            case 3:
                _renderer.material.color = Color.yellow;
                BalloonLife--;
                break;
            case 2:
                _renderer.material.color = Color.red;
                BalloonLife--;
                break;
            case 1:
                _renderer.material.color = new Color(25, 25, 25);
                gameObject.SetActive(false);
                PlayerController.instance.AddScore(3);
                BalloonLife = 5;
                break;
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
}