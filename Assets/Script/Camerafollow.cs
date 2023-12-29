using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camerafollow : MonoBehaviour
{   
    [SerializeField] private float camera_speed = 2f;
    [SerializeField] private float yoffset = -1;
    [SerializeField] private Transform target;
    void Update()
    {
        Vector3 newPosistion = new Vector3(target.position.x,target.position.y+yoffset,-10f);
        transform.position = Vector3.Slerp(transform.position,newPosistion,camera_speed*Time.deltaTime);
    }
}