using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public enum AdjustmentValueName
{
    aaa, bbb
}

//難易度を調整してくれる人
public class LevelAdjuster : SingletonMonobehaviour<LevelAdjuster>
{
    [SerializeField]
    AdjustmentValueByGameTime[] adjustmentValueByGameTimes = null;
    [SerializeField]
    AdjustmentValueByTrigger[] adjustmentValueByTriggers = null;

    Dictionary<AdjustmentValueName, IAdjustmentValue> adjustmentValueDictionary = new Dictionary<AdjustmentValueName, IAdjustmentValue>();

    void Awake()
    {
        for (int i = 0; i < adjustmentValueByGameTimes.Length; i++)
        {
            AddValue(adjustmentValueByGameTimes[i].Name, adjustmentValueByGameTimes[i]);
        }

        for (int i = 0; i < adjustmentValueByTriggers.Length; i++)
        {
            AddValue(adjustmentValueByTriggers[i].Name, adjustmentValueByTriggers[i]);
        }
        
        foreach(var key in adjustmentValueDictionary.Keys)
        {
            adjustmentValueDictionary[key].Init();
        }
    }

    void Update()
    {
        for (int i = 0; i < adjustmentValueByGameTimes.Length; i++)
        {
            adjustmentValueByGameTimes[i].Update();
        }
    }

    void AddValue(AdjustmentValueName name, IAdjustmentValue adjustmentValue)
    {
        try
        {
            adjustmentValueDictionary.Add(name, adjustmentValue);
        }
        catch
        {
            Debug.LogWarning("same name already exists");
        }
    }

    public T GetValue<T>(AdjustmentValueName name)
    {
        IAdjustmentValue adjustmentValue = null;

        if (adjustmentValueDictionary.TryGetValue(name, out adjustmentValue))
        {
            return ((AbstractAdjustmentValue<T>)adjustmentValue).CurrentValue;
        }

        return default(T);
    }

    public void LevelUp(AdjustmentValueName name)
    {
        IAdjustmentValue adjustmentValue = null;

        if (!adjustmentValueDictionary.TryGetValue(name, out adjustmentValue)) return;

        adjustmentValue.LevelUp();
    }
}
