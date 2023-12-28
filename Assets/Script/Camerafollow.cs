using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camerafollow : MonoBehaviour
{   
    public float camera_speed = 2f;
    public float yoffset = -1;
    public Transform target;
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x,target.position.y+yoffset,-10f);
        transform.position = Vector3.Slerp(transform.position,newPos,camera_speed*Time.deltaTime);
    }
}