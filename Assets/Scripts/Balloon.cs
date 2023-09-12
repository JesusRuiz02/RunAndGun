using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float _speed = default;
    [SerializeField] private Vector3 _newPosition = default;
    [SerializeField] private Transform _player = default;
    void Start()
    {
        _player = Camera.main.transform;
        _speed += PlayerController.instance.Score / 5 ;
    }

    private void Update()
    {
        _newPosition = transform.position;
        _newPosition.y += Mathf.Sin(Time.time) * Time.deltaTime;
        transform.position = _newPosition;
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }
}
