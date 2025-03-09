using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private AudioClip _popSfx = default;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            PlayerController.instance.Health();
            AudioManager.instance.SetSound(SOUND_TYPE.POP_BALLLOON);
            other.gameObject.SetActive(false);
            Camera.main.DOShakePosition(0.25f, 1, 80, 90f, true);
        }
    }

}
