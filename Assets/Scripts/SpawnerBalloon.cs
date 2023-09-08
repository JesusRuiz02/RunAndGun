using UnityEngine;

public class SpawnerBalloon : MonoBehaviour
{
    [SerializeField] private GameObject Ballons;

    public static SpawnerBalloon instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CreateBalloons()
    {
        Instantiate(Ballons, new Vector3(Random.Range(-20, 20), Random.Range(20, 5), 125),Quaternion.Euler(-90,0,0));
    }

}
