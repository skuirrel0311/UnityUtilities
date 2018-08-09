using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class StageModeScoreManager : MyScoreManager<StageModeScoreManager>
{
    RegisterIntParameter currentLevel;
    public int CurrentLevel
    {
        get
        {
            return currentLevel.GetValue();
        }
        set
        {
            currentLevel.SetValue(value);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        currentLevel = new RegisterIntParameter("CurrentLevel", 1);
    }
}
