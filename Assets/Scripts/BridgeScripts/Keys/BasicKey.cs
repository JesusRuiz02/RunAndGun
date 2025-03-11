using UnityEngine;

public class BasicKey : Keys
{
   [SerializeField] private Animator _doorAnimator;
   [SerializeField][Range(-50, 50)] private float _maxLimitX;
   [SerializeField][Range(-50, 50)] private float _maxLimitY;
   [SerializeField][Range(-50, 50)] private float _minLimitY;
   [SerializeField][Range(-50, 50)] private float _minLimitX;
   
   
   protected override void PhaseOne()
   {
      
   }

   protected override void PhaseTwo()
   {
      transform.localPosition = new Vector3(Random.Range(_minLimitX, _maxLimitX), Random.Range(_minLimitY, _maxLimitY), transform.localPosition.z);
   }

   protected override void PhaseThree()
   {
      _doorAnimator.Play("Phase2Door");
   }
}
