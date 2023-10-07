using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage_Pop_Up_Animation : MonoBehaviour
{
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;

    TextMeshProUGUI popUp;

    float time = 0;

    Vector3 origin;
    void Awake()
    {
        popUp = GetComponentInChildren<TextMeshProUGUI>();
        origin = transform.position;
    }

    
    void Update()
    {
        popUp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
        transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
        transform.position = origin + new Vector3(0.0f, 1.0f +  heightCurve.Evaluate(time), 0.0f);
        time += Time.deltaTime;
    }
}
