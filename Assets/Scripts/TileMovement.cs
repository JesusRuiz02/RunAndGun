using System;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] private Transform Tile = default;
    [SerializeField] private float _speed = default;
    public void Update()
    {
        Tile.Translate(0,0, -_speed * Time.deltaTime);

        if (Tile.position.z <= -20f)
        {
            gameObject.SetActive(false);
        }
        
    }
}
