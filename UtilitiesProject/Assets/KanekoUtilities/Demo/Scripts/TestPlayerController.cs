using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 4.0f;

    Transform myTransform;

    public bool CanMove = false;
    public Vector3 Movement { get; private set; }

    void Awake()
    {
        myTransform = transform;
        Movement = Vector3.forward;
    }

    void FixedUpdate()
    {
        if (!CanMove) return;

        //ここでスピードを計算に含まなくても、移動量が変化した時に更新すればよい
        myTransform.Translate(Movement * speed * Time.deltaTime);
    }
}
