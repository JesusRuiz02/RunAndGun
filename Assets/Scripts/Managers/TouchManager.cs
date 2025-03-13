using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
  [SerializeField] private GameObject _player;
  private PlayerInput _playerInput;
  [SerializeField] private LayerMask _shotLayerMask;
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
    if (Time.timeScale == 1 )
    {
      Vector3 position = _touchPosition.ReadValue<Vector2>();
      position.z = _player.transform.position.z;
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(position);
      if (Physics.Raycast(ray, out hit, Mathf.Infinity, _shotLayerMask))
      {
        Debug.DrawLine(ray.origin, hit.point, Color.red);
        CheckMag(hit.point);
      }
    }
  }

  private void CheckMag(Vector3 Targetposition)
  {
    GameObject projectile = ObjectPool.instance.GetPooledObject();
    
    if (projectile != null)
    {
      projectile.transform.position = _player.transform.position + new Vector3(0, 0.25f, -1);
      projectile.SetActive(true);
      PlayerController.instance.Shoot(Targetposition, projectile);
    }
    else
    {      
      GameObject provBullet = ObjectPool.instance.CreateMoreBullets();
      provBullet.transform.position = _player.transform.position + new Vector3(0, 0.25f, -1);
      provBullet.SetActive(true);
      PlayerController.instance.Shoot(Targetposition, provBullet);
    }
  }

 
}
