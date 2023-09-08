using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] private Transform Tile = default;

   

    
   // private SpawnTile _spawnTile = default;

    /*private void Start()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        _spawnTile = spawnPoint.GetComponent<SpawnTile>();
    }*/


    public void Update()
    {
        Tile.Translate(0,0, -3f * Time.deltaTime);

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
