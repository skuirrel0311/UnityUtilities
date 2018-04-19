using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public enum AdjustmentValueName
{
    //todo:ここに調整要素の名前を定義していく
}

//難易度を調整してくれる人
public class LevelAdjusterByGameTime : SingletonMonobehaviour<LevelAdjusterByGameTime>
{
    [SerializeField]
    AdjustmentValueByGameTime[] adjustmentValueByGameTimes = null;

    Dictionary<AdjustmentValueName, AdjustmentValueByGameTime> adjustmentValueDictionary = new Dictionary<AdjustmentValueName, AdjustmentValueByGameTime>();

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < adjustmentValueByGameTimes.Length; i++)
        {
            adjustmentValueDictionary.Add(adjustmentValueByGameTimes[i].Name, adjustmentValueByGameTimes[i]);
        }
    }

    public void Init()
    {
        for (int i = 0; i < adjustmentValueByGameTimes.Length; i++)
        {
            adjustmentValueByGameTimes[i].Init();
        }
    }

    public void StartAdjustment()
    {
        for (int i = 0; i < adjustmentValueByGameTimes.Length; i++)
        {
            adjustmentValueByGameTimes[i].Start();
        }
    }

    void Update()
    {
        for (int i = 0; i < adjustmentValueByGameTimes.Length; i++)
        {
            adjustmentValueByGameTimes[i].Update();
        }
    }

    public float GetCurrentValue(AdjustmentValueName name)
    {
        AdjustmentValueByGameTime adjustmentValue = GetAdjustmentValue(name);

        return adjustmentValue != null ? adjustmentValue.CurrentValue : 0.0f;
    }

    public AdjustmentValueByGameTime GetAdjustmentValue(AdjustmentValueName name)
    {
        AdjustmentValueByGameTime adjustmentValue = null;

        if (adjustmentValueDictionary.TryGetValue(name, out adjustmentValue))
        {
            return adjustmentValue;
        }
        else
        {
            return null;
        }
    }
}
