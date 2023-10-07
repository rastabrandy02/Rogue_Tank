using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Pointer : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Sprite sprite;
    [SerializeField] Sprite transparentSprite;
    RectTransform rectTransform;
    Image image;
    float borderSize = 50.0f;
    bool isOffScreen;
    bool isVisible;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        image.sprite = transparentSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) Destroy(gameObject);
        Vector3 cameraCenter = Camera.main.transform.position;      
        cameraCenter.z = 0.0f;
        Vector3 direction = (target.position - cameraCenter).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;     
        rectTransform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);

        Vector3 targetPosScreenPoint = Camera.main.WorldToScreenPoint(target.position);
        isOffScreen = targetPosScreenPoint.x <= 0 || targetPosScreenPoint.y >= Screen.width
            || targetPosScreenPoint.y <= 0 || targetPosScreenPoint.y >= Screen.height;

        if(isOffScreen)
        {
            if(!isVisible) MakeVisible();
            Vector3 cappedTargetScreenPosition = targetPosScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 pointerWorldPos = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            rectTransform.position = cappedTargetScreenPosition;
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0.0f);
        }
        else
        {
           if(isVisible) MakeInVisible();
        }
        
    }
    void MakeVisible()
    {
        image.sprite = sprite;
        isVisible = true;
    }
    void MakeInVisible()
    {
        image.sprite = transparentSprite;       
        isVisible = false;
        
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
