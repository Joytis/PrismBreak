using UnityEngine;
using System.Collections;

public class CamShakeSimple : MonoBehaviour 
{

    Vector3 originalCameraPosition;

    public float shakeAmt = 0;

    Camera mainCamera;
    // shakeAmt = coll.relativeVelocity.magnitude * .0025f;
    //     InvokeRepeating("CameraShake", 0, .01f);
    //     Invoke("StopShaking", 0.3f);

    public void InvokeTheShake(float f1, float f2)
    {
    	InvokeRepeating("CameraShake", 0, f1);
        Invoke("StopShaking", f1);
    }

    void Awake() {
    	mainCamera = Camera.main;
    	originalCameraPosition = mainCamera.gameObject.transform.position;
    }

    void CameraShake()
    {
        if(shakeAmt>0) 
        {
            float quakeAmt = shakeAmt*2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y+= quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }

}