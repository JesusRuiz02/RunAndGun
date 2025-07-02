using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Balloon/Pop Strategies/Basic")]
public class BasicPopStrategy : BallonPopStrategy
{
    public override void PopBalloon(Balloon balloon)
    {
        PlayerController.instance.AddScore(1);
        balloon.gameObject.SetActive(false);
        balloon.CreateParticula();
    }
}
