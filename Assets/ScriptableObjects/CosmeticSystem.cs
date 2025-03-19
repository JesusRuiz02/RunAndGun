using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ExternalAssets", menuName = "ScriptableObjects/Cosmetics", order = 2)]
public class CosmeticSystem : ScriptableObject
{
    [SerializeField] private List<Skin> _skins = new List<Skin>();
    [SerializeField] private List<SkinMaterial> _materials = new List<SkinMaterial>();
    
    public GameObject GetActiveSkin()
    {
        foreach (var skin in _skins)
        {
            if (skin.isActive)
            {
                return skin.gameObject;
            }
        }
        return null;
    }

    public GameObject GetSkin(int id)
    {
        foreach (var skins in _skins)
        {
            if (skins.skinId == id)
            {
                return skins.gameObject;
            }
        }
        return null;
    }
    
    public Material GetMaterialBySkinID(int skinID)
    {
        foreach (var skinMat in _materials)
        {
            if (skinMat.skinID == skinID)
                return skinMat.material;
        }
        return null; 
    }
}

[Serializable]
public struct Skin
{
    public int skinId;
    public bool isUnlocked;
    public string skinName;
    public GameObject gameObject;
    public bool isActive;
    public string price;
}
[Serializable]
public struct SkinMaterial
{
    public int skinID;  
    public Material material; 
}


public enum CurrencyType
{
    RealMoney,
    Achievement,
    GameMoney
}
