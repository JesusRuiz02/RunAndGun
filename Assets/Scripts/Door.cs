using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animatorParent;
    [SerializeField] private String _triggerString = "Open";

    private void Awake()
    {
        _animatorParent = gameObject.transform.GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
           _animatorParent.SetTrigger(_triggerString);
        }
    }

    private void OnEnable()
    {
        _animatorParent.Play("Idle");
    }

    private void OnDisable()
    {
        TilePool._Instance.GetObjectBack(TilePool._Instance.PooledObjects, 0);
    }
}
