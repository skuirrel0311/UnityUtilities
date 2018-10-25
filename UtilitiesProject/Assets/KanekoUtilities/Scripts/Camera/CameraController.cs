using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float speed = 20.0f;

    [SerializeField]
    Transform target = null;

    Vector3 startDistance;

    void Start()
    {
        startDistance = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetPos = target.position + startDistance;

        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }
}
