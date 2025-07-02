using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Balloon/Pop Strategies/BalloonExplosion")]
public class BalloonExplosionStrategy : BallonPopStrategy
{
    private List<Transform> BalloonExplosionTransform;

    public override void PopBalloon(Balloon balloon)
    {
        BalloonExplosionTransform = balloon.BalloonExplosionTransforms;
        PlayerController.instance.AddScore(2);
        for (int i = 0; i < 2; i++)
        {
            GameObject balloons = SpawnerBalloon.instance.GetPooledObject(OBSTACLE_TYPE.BalloonMobile);
            balloons.transform.position = balloon.transform.position;
            int random = Random.Range(0, 7);
            balloons.transform.DOMove(BalloonExplosionTransform[random].position, 0.4f).SetEase(Ease.Flash);
        }
        balloon.gameObject.SetActive(false);
    }
}
