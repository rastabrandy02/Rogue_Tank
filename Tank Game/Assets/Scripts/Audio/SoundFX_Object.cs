using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX_Object : MonoBehaviour
{
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying) Destroy(gameObject);
    }
}
