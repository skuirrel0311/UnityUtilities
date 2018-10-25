using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController<T> : ActorBehaviour<T> where T : ActorFacade
{
    [SerializeField]
    protected float speed = 4.0f;

    protected Transform myTransform;

    public bool CanMove = false;
    public Vector3 Movement { get; private set; }

    protected virtual void Awake()
    {
        myTransform = transform;
    }

    void FixedUpdate()
    {
        if (!CanMove) return;

        //ここでスピードを計算に含まなくても、移動量が変化した時に更新すればよい
        myTransform.Translate(Movement * speed * Time.deltaTime);
    }
}
