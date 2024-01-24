using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapobject : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    private GameObject grabbedObjects;
    private int layerindex; 
    private void Start()
    {
        layerindex = LayerMask.NameToLayer("Objects");
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(rayPoint.position,transform.right,rayDistance);
        if(hitinfo.collider!=null && hitinfo.collider.gameObject.layer == layerindex)
        {
            if(Input.GetKey(KeyCode.E) && grabbedObjects == null)
            {
                grabbedObjects = hitinfo.collider.gameObject;
                grabbedObjects.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObjects.transform.position = grabPoint.position;
                grabbedObjects.transform.SetParent(transform);
            }
            else if (Input.GetKey(KeyCode.E) && grabbedObjects != null)
            {
                grabbedObjects.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObjects.transform.SetParent(null);
                grabbedObjects = null;
            }
        }
    }
}
