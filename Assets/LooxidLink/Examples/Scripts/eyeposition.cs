using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR;
using Tobii.G2OM;
public class eyeposition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TobiiXR_EyeTrackingData eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
        if(eyeTrackingData.GazeRay.IsValid)
        {
            // The origin of the gaze ray is a 3D point
            Vector3 rayOrigin = eyeTrackingData.GazeRay.Origin;
            Debug.Log(rayOrigin);
        }
    }
}
