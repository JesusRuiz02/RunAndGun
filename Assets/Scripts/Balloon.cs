using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float _speed = default;
    [SerializeField] private AudioClip _popSFX = default;
    [SerializeField] private Transform _player = default;
    void Start()
    {
        _player = Camera.main.transform;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
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
