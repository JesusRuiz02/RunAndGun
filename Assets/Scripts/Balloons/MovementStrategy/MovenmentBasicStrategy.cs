using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Balloon/Movement Strategies/Basic")]
public class MovenmentBasicStrategy : MovementStrategy
{
   
    public override void MoveBalloon(Balloon balloon)
    {
        balloon.transform.Translate(Vector3.up * balloon.Speed * Time.deltaTime);
    }
}
