using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
  private float _flightDurationInSeconds = 3;
  [SerializeField] private GameObject _player;
  [SerializeField] private Vector3 _bulletForce;
  [SerializeField] private GameObject _bullets;
  private PlayerInput _playerInput;
  [CanBeNull] private InputAction _touchPosition;
  private InputAction _touchPressAction;

  private void Awake()
  {
    _playerInput = GetComponent<PlayerInput>();
    _touchPressAction = _playerInput.actions.FindAction("TouchPress");
    _touchPosition = _playerInput.actions.FindAction("TouchPosition");
  }

  private void OnEnable()
  {
    _touchPressAction.performed += TouchPressed;
  }

  private void OnDisable()
  {
    _touchPressAction.performed -= TouchPressed;
  }

  private void TouchPressed(InputAction.CallbackContext context)
  {
    Debug.Log("hay touch");
    Vector3 position = _touchPosition.ReadValue<Vector2>();
    position.z = _player.transform.position.z;
    Debug.Log(position);
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(position);
    if (Physics.Raycast(ray, out hit))
    {
      Shoot(hit.point);
    }

  }

  private void Shoot(Vector3 position)
  {


    GameObject projectile = ObjectPool.instance.GetPooledObject();

    if (projectile != null)
    {
      projectile.transform.position = _player.transform.position;
      projectile.SetActive(true);
      projectile.GetComponent<Rigidbody>().velocity = (position - transform.position) / 1.5f;
    }
    
  }
}
