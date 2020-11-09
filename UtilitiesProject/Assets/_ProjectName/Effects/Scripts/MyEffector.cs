using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MyEffector : SingletonMonobehaviour<MyEffector>
{
    [SerializeField]
    CameraShake cameraShake = null;

    public void CameraShake()
    {
        cameraShake.Shake();
    }
}
