using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSFX : MonoBehaviour
{
    [SerializeField] private GameObject _particle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
           GameObject particle = Instantiate(_particle, transform.position + new Vector3(0,0, 3f), Quaternion.identity);
           Destroy(particle, 2f);
        }
    }
}
