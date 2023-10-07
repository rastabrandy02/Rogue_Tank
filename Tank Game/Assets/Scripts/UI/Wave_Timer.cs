using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wave_Timer : MonoBehaviour
{
    TMP_Text text;
    int seconds;
    int minutes;

    public static string timeString;
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    
    void Update()
    {
        minutes = Mathf.FloorToInt(Time.timeSinceLevelLoad / 60);
        seconds = Mathf.FloorToInt(Time.timeSinceLevelLoad - 60 * minutes);
        timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        text.text = timeString;

       
    }
}
