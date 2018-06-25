using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class Window : UGUIParts
{
    [SerializeField]
    UGUIImage image = null;
    [SerializeField]
    Transform container = null;

    public Transform Container { get { return container; } }

    CanvasGroup group;
    public CanvasGroup Group
    {
        get
        {
            if (group == null)
            {
                group = Container.GetComponent<CanvasGroup>();
            }
            return group;
        }
    }

    public override Color Color
    {
        get
        {
            return image.Color;
        }

        set
        {
            image.Color = value;
        }
    }
    public override float Alpha
    {
        get
        {
            return Group.alpha;
        }

        set
        {
            if (Alpha == value) return;
            Group.alpha = value;
        }
    }
}
