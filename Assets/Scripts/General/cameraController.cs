using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    
    public Transform cameraSpot;

    void LateUpdate()
    {

        transform.position = cameraSpot.position;

    }

}
