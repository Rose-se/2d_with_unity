using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on UI Element!");
    }
}
