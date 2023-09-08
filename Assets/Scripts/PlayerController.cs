using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
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
            if (_health > 2)
            {
                Debug.Log("Perdiste");
            }
            else
            {
                _health--;
            }
            
        }
    }
}
