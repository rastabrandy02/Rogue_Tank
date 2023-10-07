using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance;
    
    [SerializeField] AudioSource soundFXObject;

    public AudioSource source;
    void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        
    }
    public void PlaySoundFXClip(AudioClip audioClip, Transform instanceTransform, float volume)
   {
        AudioSource audioSource = Instantiate(soundFXObject, instanceTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        
   }
    public void PlayMusic(AudioClip audioClip, float volume)
    {
        source.Stop();
        source.clip = audioClip;
        source.volume = volume;       
        source.Play();

    }
}
