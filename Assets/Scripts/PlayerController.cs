using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private GameObject canvasGameOver;
    [SerializeField] private float _health = 3;
    [SerializeField] private AudioClip _popSfx = default;
    [SerializeField] private float score = default;
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
    }
    public float Score => score;

    public void AddScore()
    {
        score++;
        float mod = score % 20;
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
}
