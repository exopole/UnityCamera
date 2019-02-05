using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerZoom : MonoBehaviour {

    public CameraController cameraController;    

    public float minDistance;
    public float maxDistance;

    public float stepZoom;

    
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            cameraController.Distance = Mathf.Clamp(cameraController.Distance - stepZoom, minDistance, maxDistance);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            cameraController.Distance = Mathf.Clamp(cameraController.Distance + stepZoom, minDistance, maxDistance); 

        }
    }




}
