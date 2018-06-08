using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TestSceneManager : MainSceneManager
{
    [SerializeField]
    Transform trans = null;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            MessageDisplayer.Instance.ShowMessage("good!", Vector2.zero, 0.3f, 0.7f);
        }
    }
}
