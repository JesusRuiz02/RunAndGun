using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "Balloon/Movement Strategies/Door")]

public class MovementDoorStrategy : MovementStrategy
{
    public override void MoveBalloon(Balloon balloon)
    {
        balloon.transform.Translate(Vector3.forward * balloon.Speed * Time.deltaTime);
    }
}
