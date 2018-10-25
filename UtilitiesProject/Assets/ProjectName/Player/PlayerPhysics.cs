using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysics : ActorPhysics<PlayerFacade>
{
    public Rigidbody Body { get; private set; }
    
    void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }

    public override void Init()
    {
        base.Init();
        Body.velocity = Vector3.zero;
    }

    public override void EnablePhysics()
    {
        Body.isKinematic = false;
    }

    public override void DisablePhysics()
    {
        Body.isKinematic = true;
        Body.velocity = Vector3.zero;
    }
}
