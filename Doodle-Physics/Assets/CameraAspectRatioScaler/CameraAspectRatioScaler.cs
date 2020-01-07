using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine;

//Reference: https://assetstore.unity.com/packages/3d/characters/camera-aspect-ratio-scaler-112348
// Responsive Camera Scaler
public class CameraAspectRatioScaler : MonoBehaviour
{

    //Reference Resolution is acquired from Phone Screen 
    public Vector2 ReferenceResolution = new Vector2(Screen.width, Screen.height); 

    // Zoom factor to fit different aspect ratios
    public Vector3 ZoomFactor = Vector3.one;

    /// Design time position
    [HideInInspector]
    public Vector3 OriginPosition;

    // Start
    void Start()
    {
        OriginPosition = transform.position;
    }

    /// Update per Frame
    void Update()
    {

        if (ReferenceResolution.y == 0 || ReferenceResolution.x == 0)
            return;

        var refRatio = ReferenceResolution.x / ReferenceResolution.y;
        var ratio = (float)Screen.width / (float)Screen.height;

        transform.position = OriginPosition + transform.forward * (1f - refRatio / ratio) * ZoomFactor.z
                                            + transform.right * (1f - refRatio / ratio) * ZoomFactor.x
                                            + transform.up * (1f - refRatio / ratio) * ZoomFactor.y;
    }
}
