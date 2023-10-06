using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private float _shots = default;
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

   

    public float Score => score;

    public void AddScore()
    {
        score++;
        _scoreText.text = score.ToString();
        float mod = score % 10;
        if (mod == 0)
        {
            TilePool._Instance.BringObjectToFront(TilePool._Instance.PooledObjects,TilePool._Instance.PooledObjects.Count-1);
        }

        float modPowerUp = score % 20;

        if (modPowerUp == 0)
        {
            SpawnerBalloon.instance.GetPooledObject(Balloon.OBSTACLE_TYPE.PowerUp);
        }
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
            StartCoroutine(PowerUp());
           other.gameObject.SetActive(false);
            //gameObject.
            //sfx power up
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        canvasGameOver.SetActive(true);
        Debug.Log("Perdiste");
        float accuracy = (_shots / score) * 10;
         double _accuracy = Math.Round(accuracy, 2);
        _textScore.text = "Score : " + score;
        _textAccuracy.text = "Accuracy : " + _accuracy + "%";
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


    IEnumerator PowerUp()
    {
        _isInmune = true;
        yield return new WaitForSeconds(7.0f);
        _isInmune = false;
    }
}
