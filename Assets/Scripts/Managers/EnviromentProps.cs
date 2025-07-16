using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentProps : MonoBehaviour
{
    [SerializeField] private List<EnviromentPropsData> enviromentPropsDataList = new List<EnviromentPropsData>();
    private void SubscriptionToTilePoolManager()
    {
        if (TilePool._Instance != null)
        {
            TilePool._Instance.OntilePoolChange += OnTileTypeChanged;
            OnTileTypeChanged(TileType.NormalTile);
        }
    }

    private void Start()
    {
        SubscriptionToTilePoolManager();
    }

    private void OnTileTypeChanged(TileType tileType)
    {
        /* foreach (var enviromentPropsData in enviromentPropsDataList)
         {
             if (enviromentPropsData.TileType == tileType)
             {
                 enviromentPropsData.EnviromentProps.SetActive(true);
             }
             else
             {
                 enviromentPropsData.EnviromentProps.SetActive(false);
             }
         }*/

        if (tileType == TileType.DesertTile)
        {
            foreach (var enviromentPropsData in enviromentPropsDataList)
            {
                if (enviromentPropsData.TileType == tileType)
                {
                    enviromentPropsData.EnviromentProps.SetActive(true);
                }
                else
                {
                    enviromentPropsData.EnviromentProps.SetActive(false);
                }
            }
        }
        else
        {
            foreach (var enviromentPropsData in enviromentPropsDataList)
            {
                if (enviromentPropsData.TileType == TileType.DesertTile)
                {
                    enviromentPropsData.EnviromentProps.SetActive(false);
                }
                else
                {
                    enviromentPropsData.EnviromentProps.SetActive(true);
                }
            }
        }
    }

}

[Serializable]
public struct EnviromentPropsData
{
    public GameObject EnviromentProps;
    public TileType TileType;
}

