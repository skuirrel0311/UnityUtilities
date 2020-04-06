using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class CameraSettings : MonoBehaviour
{
    [SerializeField]
    float angleX = 0.0f;

    [SerializeField]
    float distanceZ = 10.0f;

    [SerializeField]
    float fieldOfView = 60;

    [SerializeField]
    Camera camera = null;

    void OnValidate()
    {
        camera.fieldOfView = fieldOfView;
        transform.SetRotationX(angleX);
        camera.transform.SetLocalPositionZ(distanceZ);
    }
}
