using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private float _shots = default;
    [SerializeField] private AudioClip _song = default;
    [SerializeField] private TextMeshProUGUI _textAccuracy;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _throwUpForce;
    [SerializeField] private GameObject canvasGameOver;
    [SerializeField] private float _health = 3;
    [SerializeField] private AudioClip _popSfx = default;
    [SerializeField] private float score = default;
    [SerializeField] private TextMeshProUGUI _scoreText = default;
    [SerializeField] private GameObject _powerUP = default;
   
    private bool _isInmune = default;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _scoreText.text = score.ToString();
    }

    private void Start()
    {
        AudioManager.instance.PlayMusic(_song);
    }

    public float Score => score;

    public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
        _scoreText.text = score.ToString();
        SpawnObstacleNPowerUps();
    }

    private void SpawnObstacleNPowerUps()
    {
        bool isScoreMultiple = GetMod(20);
        if (isScoreMultiple)
        {
            TilePool._Instance.BringObjectToFront(TilePool._Instance.PooledObjects,TilePool._Instance.PooledObjects.Count-1);
        }
        isScoreMultiple = GetMod(15);
        if (isScoreMultiple)
        {
            SpawnerBalloon.instance.GetPooledObject(Balloon.OBSTACLE_TYPE.PowerUp);
        }
        isScoreMultiple = GetMod(17);
        if (isScoreMultiple)
        {
            SpawnerBalloon.instance.GetPooledObject(Balloon.OBSTACLE_TYPE.BalloonSpawner);
        }
    }

    private bool GetMod(float dividedMod)
    {
        float mod = score % dividedMod;
        bool isModZero = mod == 0 ? true : false;
        return isModZero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            Health();
            AudioManager.instance.PlaySFX(_popSfx);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("PowerUp"))
        {
            CallCoroutine();
            other.gameObject.SetActive(false);
        }

        
    }

    private void GameOver()
    {
        canvasGameOver.SetActive(true);
        Debug.Log("Perdiste");
        float accuracy = (score / _shots ) * 100;
        double _accuracy = Math.Round(accuracy, 2);
        _accuracy = score == 0 ? 0 : _accuracy; //Para que la division no de infinito en caso de ser cero
        _textScore.text = "Score : " + score;
        _textAccuracy.text = "Accuracy : " + _accuracy + "%";
        Time.timeScale = 0;
    }

    private void Health()
    {
        if (!_isInmune)
        {
            if (_health < 2)
            {
               GameOver();
            }
            else
            {
                _health--;
            }
            
        }
    }
    
    public void Shoot(Vector3 Targetposition, GameObject projectile)
    {
        _shots++;
        Vector3 forceDirection = transform.forward;
    
        forceDirection = (Targetposition - transform.position).normalized;

        Vector3 forceToAdd = forceDirection * _throwForce + transform.up * _throwUpForce;
    
        // projectile.transform.LookAt(forceDirection);
    
        projectile.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);
    }

    public void CallCoroutine()
    {
        StartCoroutine(PowerUp());
    }


    public IEnumerator PowerUp()
    {
        _isInmune = true;
        yield return new WaitForSeconds(7.0f);
        _isInmune = false;
    }
}
