using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource musicSource, SfxSource = default;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.ignoreListenerPause = true;
        SfxSource.ignoreListenerPause = true;
    }
    
    [SerializeField] SoundLibrary soundLibrary;
    GameObject prefabAudioSource;
    List<AudioSource> audioSourcesList = new List<AudioSource>();


    public void SetSound(SOUND_TYPE _sound)
    {
        SfxSource.PlayOneShot(soundLibrary.GetRandomSoundFromType(_sound));
    }

   /* public void SetSound(SOUND_TYPE _sound, Vector3 _position)
    {
        AudioSource audio = GetAudioSource();
        audio.transform.position = _position;
        audio.clip = soundLibrary.GetRandomSoundFromType(_sound);
        audio.Play();
    }
    AudioSource GetAudioSource()
    {
        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            if (!audioSourcesList[i].isPlaying)
            {
                return audioSourcesList[i];
            }
        }

        AudioSource s = Instantiate(prefabAudioSource, transform).GetComponent<AudioSource>();
        audioSourcesList.Add(s);
        return s;
    }*/
    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

}
