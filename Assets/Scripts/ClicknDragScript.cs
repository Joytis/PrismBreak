using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CamShakeSimple))]
public class ClicknDragScript : MonoBehaviour {

	// private Color mouseOverColor = Color.blue;
    // private Color originalColor = Color.yellow;
    private bool dragging = false;
    private float distance;

    CamShakeSimple camshake;
 
    void Awake() {
        camshake = gameObject.GetComponent<CamShakeSimple>();
    }
   
    void OnMouseEnter()
    {
        // renderer.material.color = mouseOverColor;
    }
 
    void OnMouseExit()
    {
        // renderer.material.color = originalColor;
    }
 
    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }
 
    void OnMouseUp()
    {
        if(dragging)
        {
            dragging = false;
            camshake.InvokeTheShake(0.05f, 0.1f);
        }
    }
 
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }
}
