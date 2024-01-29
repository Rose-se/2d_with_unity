using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapobject : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    [SerializeField] private float bounceForce = 100f;

    private GameObject grabbedObjects;
    private int layerindex; 
    private bool isGrabbing = false;

    private void Start()
    {
        layerindex = LayerMask.NameToLayer("Objects");
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
        if (hitinfo.collider != null && hitinfo.collider.gameObject.layer == layerindex)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Toggle grabbing state
                isGrabbing = !isGrabbing;

                if (isGrabbing)
                {
                    // Grab the object
                    grabbedObjects = hitinfo.collider.gameObject;
                    grabbedObjects.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabbedObjects.transform.SetParent(transform);
                }
                else
                {
                    // Release the object
                    grabbedObjects.GetComponent<Rigidbody2D>().isKinematic = false;
                    grabbedObjects.transform.SetParent(null);
                    
                    // Calculate force direction based on player's orientation
                    Vector2 forceDirection = transform.right;
                    if (GetComponent<SpriteRenderer>().flipX)
                    {
                        forceDirection = -forceDirection;
                    }

                    // Apply force to make it bounce
                    Rigidbody2D grabbedRigidbody = grabbedObjects.GetComponent<Rigidbody2D>();
                    grabbedRigidbody.AddForce(forceDirection * bounceForce, ForceMode2D.Impulse);

                    grabbedObjects = null;
                }
            }
        }

        // Move the grabbed object to the grab point
        if (isGrabbing && grabbedObjects != null)
        {
            grabbedObjects.transform.position = grabPoint.position;
        }
    }
}
