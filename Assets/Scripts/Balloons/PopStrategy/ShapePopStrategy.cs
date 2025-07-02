using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Balloon/Pop Strategies/ShapePop")]
public class ShapePopStrategy : BallonPopStrategy
{
     private List<Transform> BalloonExplosionTransform;

    public override void PopBalloon(Balloon balloon)
    {
        BalloonExplosionTransform = balloon.BalloonExplosionTransforms;
        PlayerController.instance.AddScore(1);
        foreach (var explosionTransform in BalloonExplosionTransform)
        {
            GameObject balloons = SpawnerBalloon.instance.GetPooledObject(OBSTACLE_TYPE.BalloonMobile);
            balloons.transform.position = balloon.transform.position;
            balloons.transform.DOMove(explosionTransform.position, 0.4f).SetEase(Ease.Flash);
        }
        balloon.gameObject.SetActive(false);
    }
}
