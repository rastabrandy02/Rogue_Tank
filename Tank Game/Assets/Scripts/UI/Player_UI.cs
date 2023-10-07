using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UI : MonoBehaviour
{
    [SerializeField]
    Transform turret;
    [SerializeField]
    float turretRotationSpeed;
    Camera cam;
    Vector3 mousePos;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RotateTurret(); 
    }

    void RotateTurret()
    {

        Vector3 direction = mousePos - turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        Quaternion finalRotation = Quaternion.RotateTowards(turret.rotation, Quaternion.Euler(0, 0, angle), turretRotationSpeed * Time.deltaTime);
        turret.rotation = finalRotation;
    }
}
