using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class Test : MonoBehaviour
{
    [SerializeField]
    UGUITextUnity text = null;
    void Update()
    {
        text.Text = StageModeGameManager.Instance.CurrentState.ToString();
    }
}
