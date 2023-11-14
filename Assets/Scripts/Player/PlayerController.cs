using System;
using System.Collections;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField]  private GameObject _canvasPause;
    [SerializeField] private AudioClip BulletSfx;
    private float _shots = default;
    [SerializeField] private AudioClip _popSfx = default;
    [SerializeField] private AudioClip _song = default;
    [SerializeField] private TextMeshProUGUI _textAccuracy;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _throwUpForce;
    [SerializeField] private GameObject canvasGameOver; 
    [SerializeField] private float score = default;
    [SerializeField] private TextMeshProUGUI _scoreText = default;
    private HealthController _healthController = default;
    public float Score => score;
    private bool _isInmune = default;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _scoreText.text = score.ToString();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlayMusic(_song);
        _healthController = gameObject.GetComponent<HealthController>();
    }
    

    public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
        _scoreText.text = score.ToString();
        if (score >= 100)
        {
            GameManager.instance.SpawnObstacleNPowerUps(16f,OBSTACLE_TYPE.HeavyBalloon);
        }
        if (score >= 0)
        {
            GameManager.instance.SpawnObstacleNPowerUps(25f,OBSTACLE_TYPE.ShapeBalloon);
        }
        GameManager.instance.SpawnObstacleNPowerUps(9f,OBSTACLE_TYPE.BalloonSpawner);
        GameManager.instance.SpawnObstacleNPowerUps(30f,OBSTACLE_TYPE.PowerUp);
        GameManager.instance.SpawnObstacleNPowerUps(10f, OBSTACLE_TYPE.HealPowerUp);
        GameManager.instance.SpawnObstacleNPowerUps(36f, OBSTACLE_TYPE.ExtraLifePowerUp);
        GameManager.instance.SpawnObstacleNPowerUps(38f, OBSTACLE_TYPE.Door);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            Health();
            AudioManager.instance.PlaySFX(_popSfx);
            other.gameObject.SetActive(false);
            Camera.main.DOShakePosition(0.25f, new Vector3(0, 2, 0), 80, 90f, true);
        }
    }


    public void GameOver()
    {
        canvasGameOver.SetActive(true);
        _canvasPause.SetActive(false);
        float highScore = PlayerPrefs.GetFloat("highscore", score);
        float accuracy = (score / _shots ) * 100;
        double _accuracy = Math.Round(accuracy, 2);
        _accuracy = score == 0 ? 0 : _accuracy; //Para que la division no de infinito en caso de ser cero
        if (highScore < score)
        {
            PlayerPrefs.SetFloat("highScore", score);
        }
        _textScore.text = "Highscore : " + highScore;
        _textAccuracy.text = "Accuracy : " + _accuracy + "%";
        Time.timeScale = 0;
    }

    public void Health()
    {
        if (!_isInmune)
        {
            _healthController.ReduceHealth();
        }
    }
    
    public void Shoot(Vector3 Targetposition, GameObject projectile)
    {
        _shots++;
        Vector3 forceDirection = transform.forward;
    
        forceDirection = (Targetposition - transform.position).normalized;

        Vector3 forceToAdd = forceDirection * _throwForce + transform.up * _throwUpForce;

        projectile.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);
        
        AudioManager.instance.PlaySFX(BulletSfx);
    }

    public void CallCoroutine()
    {
        StartCoroutine(PowerUp());
    }

    public void callHealPW()
    {
        _healthController.Heal();
    }

    public void callExtraLife()
    {
        _healthController.AddExtraLife();
    }

    public IEnumerator PowerUp()
    {
        _isInmune = true;
        yield return new WaitForSeconds(7.0f);
        _isInmune = false;
    }
}
