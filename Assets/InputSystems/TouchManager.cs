using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
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
     Vector3 position = Camera.main.ScreenToWorldPoint(_touchPosition.ReadValue<Vector2>());
     //position.z = _player.transform.position.z;
     Debug.Log(position);
     Shoot(position);
  }

  private void Shoot(Vector3 position)
  {
    GameObject Projectile = Instantiate(_bullets, position, Quaternion.identity);
    Projectile.GetComponent<Rigidbody>().AddForce(_bulletForce, ForceMode.Acceleration);
  }
}
