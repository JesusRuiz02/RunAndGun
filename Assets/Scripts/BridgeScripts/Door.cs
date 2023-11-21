using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animatorParent;
    private bool _isOpened;
    public bool IsOpened => _isOpened;
    [SerializeField] private String _triggerString = "Open";
    [SerializeField] private List<GameObject> _keysList = new List<GameObject>();

    private void Awake()
    {
        _animatorParent = gameObject.transform.GetComponentInParent<Animator>();
    }

    public void DoorCheck()
    {
        bool keysAreInactive = true;
        foreach (var keys in _keysList)
        {
            keysAreInactive = keys.activeInHierarchy;
        }
        if (!keysAreInactive)
        {
            _animatorParent.SetTrigger(_triggerString);
        }
        _isOpened = !keysAreInactive;
    }
    
    
    private void OnEnable()
    {
        foreach (var keys in _keysList)
        {
           keys.SetActive(true);  
        }
        _animatorParent.Play("Regresar");
    }

   
}
