using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    [SerializeField] private List<LightningData> lightningDataList = new List<LightningData>();
    [SerializeField] private SpriteRenderer lightningSpriteRenderer = default;


    private void Start()
    {
        SubscriptionToTilePoolManager();
    }

    private void SubscriptionToTilePoolManager()
    {
        if (TilePool._Instance != null)
        {
            TilePool._Instance.OntilePoolChange += OnTileTypeChanged;
            OnTileTypeChanged(TileType.NormalTile);
        }
    }

    private void OnTileTypeChanged(TileType tileType)
    {
        foreach (var lightningData in lightningDataList)
        {
            if (lightningData.TileType == tileType)
            {
                RenderSettings.sun = lightningData.LightningPrefab;
                RenderSettings.fogColor = lightningData.LightningFog;
                lightningSpriteRenderer.color = lightningData.LightningFog;
                RenderSettings.fog = lightningData.IsFogEnabled; 
                break;
            }
        }
    }
   
    
}

[Serializable]
public struct LightningData
{
    public Light LightningPrefab;
    public bool IsFogEnabled;
    public Color LightningFog;
    public TileType TileType;

}
