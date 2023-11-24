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
        int keysClose = 0;
        foreach (var keys in _keysList)
        {
            keysClose += keys.activeInHierarchy ? 1 : 0;
            Debug.Log(keysClose);
        }
        if (keysClose == 0)
        {
            _animatorParent.SetTrigger(_triggerString);
            _isOpened = true;
        }
      
    }
    
    
    private void OnEnable()
    {
        foreach (var keys in _keysList)
        {
           keys.SetActive(true);  
        }
        _isOpened = false;
        _animatorParent.Play("Regresar");
    }

   
}
