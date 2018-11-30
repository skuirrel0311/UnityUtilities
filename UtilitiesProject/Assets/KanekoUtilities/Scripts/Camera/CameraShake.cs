using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public void Shake()
    {
        transform.DOShakePosition(0.2f, 2.5f, 50).SetUpdate(true);
    }
}
