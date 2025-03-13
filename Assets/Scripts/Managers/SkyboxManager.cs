using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] private SkyboxScriptableObject skyboxScriptableObject;
    private static SkyboxManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SkyboxManager GetInstance => instance;

    public void SwitchSkybox(TileType tileType)
    {
        SwitchSkybox((skyboxScriptableObject.GetSkybox(tileType),0));
    }
    
    public void SwitchSkybox(Material newSkybox)
    {
       SwitchSkybox((newSkybox,0));
    }
    
    public void SwitchSkybox((TileType tileType,float delay) newSkyboxData)
    {
        StartCoroutine(CoroutineSwitchSkybox(
            (skyboxScriptableObject.GetSkybox(newSkyboxData.tileType), newSkyboxData.delay)
        ));
    }
    
    public void SwitchSkybox((Material newSkybox, float delay) newSkyboxData)
    {
       StartCoroutine(CoroutineSwitchSkybox(newSkyboxData));
    }

    private IEnumerator CoroutineSwitchSkybox((Material newSkybox, float delay) newSkyboxData)
    {
        yield return new WaitForSeconds(newSkyboxData.delay);
        RenderSettings.skybox = newSkyboxData.newSkybox;
        DynamicGI.UpdateEnvironment();
    }
}
