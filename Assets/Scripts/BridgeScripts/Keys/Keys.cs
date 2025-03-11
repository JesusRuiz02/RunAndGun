using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private List<KeysBehaviours> valuesToChangeBehaviour = new List<KeysBehaviours>();
    [SerializeField] private GameObject _explosionParticle;
     private DoorBehaviour? doorBehaviour = null;
    [SerializeField] private Transform _explosionTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
            _door.DoorCheck();
            GameObject particle = Instantiate(_explosionParticle, _explosionTransform.position, Quaternion.identity);
            Destroy(particle, 1f);
           
        }
    }

    private void ChangeBehaviour()
    {
        switch (doorBehaviour)
        {
            case DoorBehaviour.PHASE_1:
                PhaseOne();
                break;
            case DoorBehaviour.PHASE_2:
                PhaseTwo();
                break;
            case DoorBehaviour.PHASE_3:
                PhaseThree();
                break;
                
            
        }
    }

    protected virtual void PhaseOne()
    {
        
    }

    protected virtual void PhaseTwo()
    {
        
    }

    protected virtual void PhaseThree()
    {
        
    }

    private void OnEnable()
    {
        doorBehaviour = valuesToChangeBehaviour
            .Select(b => b.ReturnKeysBehaviour((int)PlayerController.instance.Score)) 
            .FirstOrDefault(b => b.HasValue) ?? DoorBehaviour.PHASE_1;
        ChangeBehaviour();
    }
}

public enum DoorBehaviour{
    PHASE_1,
    PHASE_2,
    PHASE_3,
}

[System.Serializable]
class KeysBehaviours
{
    public DoorBehaviour doorBehaviour;
    public int inferiorLimitToChangeBehaviour;
    public int superiorLimitToChangeBehaviour;
    public bool initialPhase;
    public bool lastPhase;

    public DoorBehaviour? ReturnKeysBehaviour(int score)
    {
        if (initialPhase)
        {
            if (score < superiorLimitToChangeBehaviour)
            {
                return doorBehaviour;
            }
        }
        else if (lastPhase)
        {
            if (score >= inferiorLimitToChangeBehaviour)
            {
                return doorBehaviour;
            }
        }
        else if (score >= inferiorLimitToChangeBehaviour && score < superiorLimitToChangeBehaviour)
        {
            return doorBehaviour;
        }
        return null;
    }
    
}
