using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Sound : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    float timeSinceLastSound;
    float timeBetweenSounds = 0.75f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSound += Time.unscaledDeltaTime;
    }

    public void PlayAudio()
    {
        if(gameObject.activeInHierarchy)
        Audio_Manager.instance.PlaySoundFXClip(clip, transform, 1);
    }

    public void PlayAudioOnSlider()
    {
        if(timeSinceLastSound >= timeBetweenSounds)
        {
            Audio_Manager.instance.PlaySoundFXClip(clip, transform, 1);
            timeSinceLastSound = 0.0f;
        }
    }
}
