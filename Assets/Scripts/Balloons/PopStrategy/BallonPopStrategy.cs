using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallonPopStrategy : ScriptableObject, IPop
{
    public abstract void PopBalloon(Balloon balloon);
}


