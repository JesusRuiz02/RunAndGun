using UnityEngine;

public class ShopSystem : MonoBehaviour
{
 [SerializeField] private  CosmeticSystem cosmeticSystem;
 private GameObject _activeSkin;
 [SerializeField] GameObject defaultSkin;
  private Material _skinMaterial;
 [SerializeField] Material defaultSkinMaterial;

 private void Start()
 {
     _activeSkin = cosmeticSystem.GetSkin(0);
     _skinMaterial = cosmeticSystem.GetMaterialBySkinID(PlayGamesManager.GetInstance().connectedToGamePlay ? 1 : 0);
 }
}
