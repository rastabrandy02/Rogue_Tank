using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    [SerializeField] float cameraSpeed;
    [SerializeField] Vector2 maxBoundaries;
    [SerializeField] Vector2 minBoundaries;
    [SerializeField] float minDistanceCursorPlayer;
    [SerializeField] float maxDistanceCursorPlayer;
   
    float distancePlayerMouse;
    GameObject player;
    Vector2 offset;
    Vector2 mousePos;
    Vector2 targetPos;

    Vector3 screenData;
    

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distancePlayerMouse = Vector2.Distance(mousePos, player.transform.position);
    }
    void FixedUpdate()
    {
        
        if (distancePlayerMouse >= minDistanceCursorPlayer && distancePlayerMouse <= maxDistanceCursorPlayer)
        {
            offset = GetMiddlePointPlayerCursor(player.transform.position, mousePos);
        }
        targetPos = player.transform.position;
        

        
        Vector2 desiredPos = targetPos + offset;
        CheckCameraBoundaries(desiredPos);
        Vector2 smoothTarget = Vector2.Lerp(transform.position, desiredPos, cameraSpeed);
        transform.position = new Vector3(smoothTarget.x, smoothTarget.y, -10);

       
    }
    void CheckCameraBoundaries(Vector2 checkPos)
    {
        targetPos = checkPos;
        targetPos.x = Mathf.Clamp(targetPos.x, minBoundaries.x, maxBoundaries.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minBoundaries.y, maxBoundaries.y);
    }

    Vector2 GetMiddlePointPlayerCursor(Vector2 a, Vector2 b)
    {
        
        return (b-a)/2;
    }
    Vector2 GetClosestPointToCursor(Vector2 target)
    {

        return (mousePos - (Vector2)player.transform.position).normalized * minDistanceCursorPlayer;
    }
    void MoveToPlayer()
    {

    }
}
