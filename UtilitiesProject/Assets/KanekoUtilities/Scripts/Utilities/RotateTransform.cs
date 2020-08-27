using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class RotateTransform : MonoBehaviour
    {
        [SerializeField]
        Vector3 angle = Vector3.forward;

        [SerializeField]
        float speed = 100.0f;

        void Update()
        {
            transform.Rotate(angle * speed * Time.deltaTime);
        }
    }
}