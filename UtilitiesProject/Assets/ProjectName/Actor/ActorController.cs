using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController<T> : ActorBehaviour<T> where T : ActorFacade
{
    protected Transform myTransform;

    protected virtual void Awake()
    {
        myTransform = transform;
    }
}
