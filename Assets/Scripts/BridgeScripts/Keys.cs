using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private GameObject _explosionParticle;
    [SerializeField] private Transform _explosionTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
            GameObject particle = Instantiate(_explosionParticle, _explosionTransform.position, Quaternion.identity);
            Destroy(particle, 1f);
            _door.DoorCheck();
        }
    }
}
