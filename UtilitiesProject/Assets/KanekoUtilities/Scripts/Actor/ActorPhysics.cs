using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPhysics<T> : ActorBehaviour<T> where T : ActorFacade
{
    [SerializeField]
    Collider[] cols = null;


    public virtual void EnablePhysics()
    {
    }

    public virtual void DisablePhysics()
    {
    }
}
