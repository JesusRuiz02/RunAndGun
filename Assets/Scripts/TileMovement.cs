using System;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] private Transform Tile = default;
    [SerializeField] private float _speed = default;
   

    
   // private SpawnTile _spawnTile = default;

 /*  private void Start()
   {
       _speed += PlayerController.instance.Score;
   }*/


   public void Update()
    {
        Tile.Translate(0,0, -_speed * Time.deltaTime);

        if (Tile.position.z <= -20f)
        {
            gameObject.SetActive(false);
        }
        
    }

    

   /* private void OnTriggerEnter(Collider collision)
    {
            
        if (collision.gameObject.CompareTag("End"))
        {
            //_spawnTile.Tiles();
            gameObject.SetActive(false);
        }
    }*/
}
