using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIdentifier : MonoBehaviour
{
    [SerializeField] private string _tileID = "";
    public string TileID => _tileID;
}
