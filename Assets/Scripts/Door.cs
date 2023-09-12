using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animatorParent;

    private void Start()
    {
        _animatorParent = gameObject.transform.GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
           _animatorParent.SetTrigger("Open");
        }
    }
}
