using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable_VFX : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMe()
    {
        Destroy(gameObject);

    }

    public void PlaySoundFX()
    {
        Audio_Manager.instance.PlaySoundFXClip(audioClip, transform, 0.5f);
    }
}
