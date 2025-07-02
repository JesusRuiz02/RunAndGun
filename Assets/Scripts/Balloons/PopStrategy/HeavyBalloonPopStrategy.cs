using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Balloon/Pop Strategies/HeavyPop")]
public class HeavyBalloonPopStrategy : BallonPopStrategy
{
    private int BalloonLife = 5;
    private Renderer _renderer;
    public override void PopBalloon(Balloon balloon)
    {
        _renderer = balloon.GetComponent<Renderer>();
        balloon.transform
           .DOMove(new Vector3(balloon.transform.position.x, balloon.transform.position.y + -0.2f, balloon.transform.position.z), 0.5f).SetEase(Ease.Flash);
        switch (BalloonLife)
        {
            case 5:
                _renderer.material.color = Color.black;
                BalloonLife--;
                break;
            case 4:
                _renderer.material.color = Color.green;
                BalloonLife--;
                break;
            case 3:
                _renderer.material.color = Color.yellow;
                BalloonLife--;
                break;
            case 2:
                _renderer.material.color = Color.red;
                BalloonLife--;
                break;
            case 1:
                _renderer.material.color = new Color(25, 25, 25);
                PlayerController.instance.AddScore(3);
                BalloonLife = 5;
                balloon.gameObject.SetActive(false);
                break;
        }

    }

}
