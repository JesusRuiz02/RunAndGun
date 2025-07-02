using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Balloon/Movement Strategies/Osscilating")]
public class OsscilatingMovementStrategy : MovementStrategy
{
    private Vector3 _newPosition = default;
    public override void MoveBalloon(Balloon balloon)
    {
        _newPosition = balloon.transform.position;
        _newPosition.y += Mathf.Sin(Time.time) * Time.deltaTime;
        balloon.transform.position = _newPosition;
    }
}
