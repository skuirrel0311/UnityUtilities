using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("aaa = " + LevelAdjuster.I.GetValue<float>(AdjustmentValueName.aaa));
            Debug.Log("bbb = " + LevelAdjuster.I.GetValue<int>(AdjustmentValueName.bbb));
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            LevelAdjuster.I.LevelUp(AdjustmentValueName.bbb);
        }
    }
}