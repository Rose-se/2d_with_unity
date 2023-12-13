using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flameball : MonoBehaviour //Name of object
{
    public GameObject Flame;
    public Transform shotpoint;
    public float offset;
    private float timeBtwshots;
    public float startTimeBtwashots;
    private void Update()
    {
    Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    float rotZ = Mathf.Atan2(difference.y,difference.x)* Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0f,0f,rotZ + offset);
    if(timeBtwshots <= 0){
    if(Input.GetMouseButtonDown(0))
        {
        Instantiate(Flame,shotpoint.position,transform.rotation);
        timeBtwshots = startTimeBtwashots;
        }
       }
    }
}
