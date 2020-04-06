using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class DebugPanel : Panel
{
    [SerializeField]
    UGUIButton showButton = null;

    [SerializeField]
    UGUIButton hideButton = null;

    void Start()
    {
        showButton.AddListener(Activate);
        hideButton.AddListener(Deactivate);
    }
}
