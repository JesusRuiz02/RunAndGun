using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float secondsToDeactivate;
    private void OnEnable()
    {
       Invoke("DeactivateObject", secondsToDeactivate);
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
