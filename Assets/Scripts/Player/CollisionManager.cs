using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject _crashParticle;
    [SerializeField] private AudioClip _popSfx = default;
    [SerializeField] private HealthController _healthController = default;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            _healthController.ReduceHealth();
            PlayerController.instance.Health();
            AudioManager.instance.PlaySFX(_popSfx);
            other.gameObject.SetActive(false);
            Camera.main.DOShakePosition(0.25f, new Vector3(0, 2, 0), 80, 90f, true);
        }
        
        if (other.CompareTag("Wall"))
        {
            other.gameObject.SetActive(false);
            GameObject particle = Instantiate(_crashParticle, other.transform.position, other.transform.rotation);
            Destroy(particle, 1f);
            PlayerController.instance.GameOver();
        }
    }

}
