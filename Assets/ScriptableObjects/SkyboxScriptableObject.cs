using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExternalAssets", menuName = "ScriptableObjects/SkyBoxes", order = 2)]
public class SkyboxScriptableObject : ScriptableObject
{
  [SerializeField] private List<Skyboxes> skyboxes = new List<Skyboxes>();

     public Material GetSkybox(TileType tileType)
        {
            foreach (var skybox in skyboxes)
            {
                if (tileType == skybox.enviroment)
                {
                    return skybox.skybox;
                }
            }
            return null;
        }
     
}
[Serializable]
public struct Skyboxes
{
    public TileType enviroment;
    public Material skybox;

}
