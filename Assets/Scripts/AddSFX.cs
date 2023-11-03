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
           GameObject particle = Instantiate(_particle, new Vector3(transform.position.x , transform.position.y, other.transform.position.z + 2f), Quaternion.identity);
           Destroy(particle, 2f);
        }
    }
}
