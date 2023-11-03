using System;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;


public class VolumeManager : MonoBehaviour
{
    public Slider _musicSlider = default;
    public Slider _SfxSlider = default;
    [SerializeField] private AudioSource musicSource, SfxSource = default;
    void Start()
    {
        GameObject musicGameObject = GameObject.FindGameObjectWithTag("Music");
        GameObject sfxGameObject = GameObject.FindGameObjectWithTag("SFX");
        musicSource = musicGameObject.GetComponent<AudioSource>();
        SfxSource = sfxGameObject.GetComponent<AudioSource>();
        _musicSlider.value =  PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicSource.volume = _musicSlider.value;
        _SfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
        SfxSource.volume = _SfxSlider.value;
    }
    public void ChangeMusicSlider(float value)
    {
        _musicSlider.value = value;
        PlayerPrefs.SetFloat("MusicVolume",_musicSlider.value);
        musicSource.volume = _musicSlider.value;
    }

    public void ChangeSfxSlider(float value)
    {
        _SfxSlider.value = value;
        PlayerPrefs.SetFloat("SfxVolume", _SfxSlider.value);
        SfxSource.volume = _SfxSlider.value;
    }
}
