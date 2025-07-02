using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementStrategy : ScriptableObject, IMovementBalloon
{
    public abstract void MoveBalloon(Balloon balloon);
  
}
