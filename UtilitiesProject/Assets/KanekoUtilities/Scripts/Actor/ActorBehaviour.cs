using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBehaviour<T> : MonoBehaviour where T : ActorFacade
{
    protected T owner;

    public virtual void SetOwner(T owner)
    {
        this.owner = owner;
    }
    public virtual void Init() { }
}
