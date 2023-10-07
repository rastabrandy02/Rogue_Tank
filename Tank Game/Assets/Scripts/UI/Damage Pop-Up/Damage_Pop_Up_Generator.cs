using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage_Pop_Up_Generator : MonoBehaviour
{
    [SerializeField]
    GameObject textPrefab;

    [SerializeField]
    float popUpLifespan;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void CreatePopUp(Vector3 pos, float value, Color color)
    {
        Vector3 offset = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0);

        var popUp = Instantiate(textPrefab, pos + offset, Quaternion.identity);
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = Mathf.RoundToInt(value).ToString();
        popUp.GetComponentInChildren<TextMeshProUGUI>().faceColor = color;
        

        Destroy(popUp, popUpLifespan);
    }
}
