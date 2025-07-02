using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Balloon/Movement Strategies/Towards")]
public class MovementTowarsPlayerStrategy : MovementStrategy
{
   
    public override void MoveBalloon(Balloon balloon)
    {
        balloon.transform.position = Vector3.MoveTowards(balloon.transform.position, balloon.Player.position, balloon.Speed * Time.deltaTime);
    }
}
