using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private AudioClip _popSFX = default; 
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            AudioManager.instance.PlaySFX(_popSFX);
           Destroy(gameObject);
        }
    }
}
