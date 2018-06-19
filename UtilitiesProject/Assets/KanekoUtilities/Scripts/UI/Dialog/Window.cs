using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

[RequireComponent(typeof(CanvasGroup))]
public class Window : UGUIParts
{
    [SerializeField]
    UGUIImage limImage = null;

    CanvasGroup group;
    public CanvasGroup Group
    {
        get
        {
            if (group == null)
            {
                group = GetComponent<CanvasGroup>();
            }
            return group;
        }
    }

    public override Color Color
    {
        get
        {
            return limImage.Color;
        }

        set
        {
            limImage.Color = value;
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
