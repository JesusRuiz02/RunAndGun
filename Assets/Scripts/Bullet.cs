using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float secondsToDeactivate;
    private Rigidbody _rigidbody;
    [SerializeField] private AudioClip _popSFX = default;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            PlayerController.instance.AddScore();
           DeactivateObject();
           AudioManager.instance.PlaySFX(_popSFX);
           Destroy(other.gameObject);
        }
    }

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
       Invoke("DeactivateObject", secondsToDeactivate);
    }

    private void OnDisable()
    {
        CancelInvoke("DeactivateObject");
        _rigidbody.velocity = Vector3.zero;
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
