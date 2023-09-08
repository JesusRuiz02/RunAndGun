using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float _speed = default;
    [SerializeField] private Transform _player = default;
    void Start()
    {
        _player = Camera.main.transform;
        _speed += PlayerController.instance.Score / 5 ;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }
}
