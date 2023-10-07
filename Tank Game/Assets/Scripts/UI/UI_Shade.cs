using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shade : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float maxPos;
    [SerializeField]
    float delay;
    [SerializeField]
    Image image;
    [SerializeField]
    float fadeSpeed;

    RectTransform trans;
    float elapsedTime;
    bool isWaiting;
    Vector3 startPos;
    
    void Start()
    {
        trans = GetComponent<RectTransform>();
        startPos = trans.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isWaiting) Waiting();
        
        else ShadeAnimation();
    }
    void ShadeAnimation()
    {   
        if (trans.localPosition.x < startPos.x + maxPos)
        {
            trans.localPosition += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);

            if (trans.localPosition.x > startPos.x + maxPos / 1.25f)
            {
                StopAllCoroutines();
                StartCoroutine(MakeInVisible(fadeSpeed));
            }
        }
       
        else
        {
            
           
            trans.localPosition = startPos;
            isWaiting = true;
        }

    }
    void Waiting()
    {
        if (elapsedTime >= delay)
        {
            elapsedTime = 0;
            StopAllCoroutines();
            StartCoroutine(MakeVisible(fadeSpeed));

            isWaiting = false;
        }
        else
        {
            image.color = new Color(255f, 255f, 255f, 0.0f);
            elapsedTime += Time.deltaTime;
        }

    }

    IEnumerator MakeVisible(float fadeSpeed)
    {
        float alphaValue = image.color.a;
        while (image.color.a < 1)
        {
            alphaValue += Time.deltaTime / fadeSpeed;
            image.color = new Color(255f, 255f, 255f, alphaValue);
            yield return null;
        }
    }
    IEnumerator MakeInVisible(float fadeSpeed)
    {
        float alphaValue = image.color.a;
        while (image.color.a > 0)
        {
            alphaValue -= Time.deltaTime / fadeSpeed;
            image.color = new Color(255f, 255f, 255f, alphaValue);
            yield return null;
        }
    }
}
