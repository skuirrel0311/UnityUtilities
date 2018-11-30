using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorVisualChanger<T> : ActorBehaviour<T> where T : ActorFacade
{
    [SerializeField]
    GameObject visual = null;

    public virtual void UpdateVisual(GameObject visualPrefab)
    {
        Transform visualParent = visual.transform.parent;

        Destroy(visual);
        visual = Instantiate(visualPrefab, visualParent);
    }
}
