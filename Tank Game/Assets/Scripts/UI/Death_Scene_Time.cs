using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Death_Scene_Time : MonoBehaviour
{
    TMP_Text text;
    string timeString;
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        timeString = Wave_Timer.timeString;
        text.text = timeString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
