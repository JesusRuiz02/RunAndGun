using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            Health();
            AudioManager.instance.PlaySFX(_popSfx);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("PowerUp"))
        {
            StartCoroutine(PowerUp());
            Destroy(other.gameObject);
            //gameObject.
            //sfx power up
        }
    }

    private void Health()
    {
        if (!_isInmune)
        {
            if (_health < 2)
            {
                Time.timeScale = 0;
                canvasGameOver.SetActive(true);
                Debug.Log("Perdiste");
            }
            else
            {
                _health--;
            }
            
        }
    }

    IEnumerator PowerUp()
    {
        _isInmune = true;
        yield return new WaitForSeconds(7.0f);
        _isInmune = false;
    }
}
