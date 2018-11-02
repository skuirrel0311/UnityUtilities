using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class StagePart : PoolMonoBehaviour
{
    StagePartPool owner;

    float distanceToCamera;

    public void SetOwner(StagePartPool owner)
    {
        this.owner = owner;
    }

    public void ReturnInstance()
    {
        owner.ReturnInstance(this);
    }
}
