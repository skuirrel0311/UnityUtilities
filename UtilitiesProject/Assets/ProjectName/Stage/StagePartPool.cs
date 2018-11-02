using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class StagePartPool : ObjectPool<StagePart>
{
    public override StagePart GetInstance()
    {
        StagePart part = base.GetInstance();
        part.SetOwner(this);
        return part;
    }
}
