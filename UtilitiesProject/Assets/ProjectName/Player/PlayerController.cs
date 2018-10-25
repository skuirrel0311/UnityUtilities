using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ActorController<PlayerFacade>
{
    Vector3 startPosition;
    Quaternion startRotation;

    protected override void Awake()
    {
        base.Awake();
        startPosition = myTransform.position;
        startRotation = myTransform.rotation;
    }

    public override void Init()
    {
        base.Init();
        myTransform.SetPositionAndRotation(startPosition, startRotation);
    }
}
