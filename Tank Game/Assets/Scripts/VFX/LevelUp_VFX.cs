using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp_VFX : Destroyable_VFX
{
    [SerializeField] AudioClip levelUpSFX;
    Player_Leveling playerLeveling;
    void Start()
    {
        playerLeveling = GameObject.FindWithTag("Player").GetComponent<Player_Leveling>();

        Audio_Manager.instance.PlaySoundFXClip(levelUpSFX, transform, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationEnd()
    {
        playerLeveling.LevelUp();
    }
}
