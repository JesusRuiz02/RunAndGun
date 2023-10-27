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
            if (other.GetComponent<Balloon>()._Obstacle_Type == Balloon.OBSTACLE_TYPE.BalloonSpawner)
            {
                PlayerController.instance.AddScore(1);
                other.GetComponent<Balloon>().BalloonExplosion();
            }
            PlayerController.instance.AddScore(1);
           DeactivateObject();
           AudioManager.instance.PlaySFX(_popSFX);
           other.gameObject.SetActive(false);
        }
        
        if (other.CompareTag("PowerUp"))
        {
            PlayerController.instance.CallCoroutine();
            other.gameObject.SetActive(false);
            //gameObject.
            //sfx power up
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
