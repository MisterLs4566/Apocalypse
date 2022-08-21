using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cameraSpot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    void LateUpdate()
    {
        transform.position = cameraSpot.position;
    }
}
