using System.Collections;
using UnityEngine;

public class HorizontalKeys : Keys
{
    [SerializeField] private Animator _doorAnimator;
    [SerializeField][Range(-50, 50)] private float _maxLimitY;
    [SerializeField][Range(-50, 50)] private float _minLimitY;
   
   
    protected override void PhaseOne()
    {
      
    }

    protected override void PhaseTwo()
    {
        transform.localPosition = new Vector3( 0, Random.Range(_minLimitY, _maxLimitY),transform.localPosition.z);
    }

    protected override void PhaseThree()
    {
        _doorAnimator.Play("Phase2Door");
    }
}
