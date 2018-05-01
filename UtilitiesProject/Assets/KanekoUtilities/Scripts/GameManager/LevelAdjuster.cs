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
    public class LevelAdjuster : SingletonMonobehaviour<LevelAdjuster>
    {
        [SerializeField]
        AdjustmentValueByGameTime[] adjustmentValuesByGameTime = null;

        [SerializeField]
        AdjustmentValueByTrigger[] adjustmentValuesByTrigger = null;

        Dictionary<AdjustmentValueName, IAdjustmentValue> AdjustmentValueDic = new Dictionary<AdjustmentValueName, IAdjustmentValue>();

        protected override void Awake()
        {
            base.Awake();

            AddDictionary(adjustmentValuesByGameTime);
            AddDictionary(adjustmentValuesByTrigger);
        }

        void AddDictionary(IAdjustmentValue[] adjustmentValues)
        {
            for (int i = 0; i < adjustmentValues.Length; i++)
            {
                IAdjustmentValue value = adjustmentValues[i];
                AdjustmentValueDic[value.Name] = value;
            }
        }

        public void Init()
        {
            foreach (var key in AdjustmentValueDic.Keys)
            {
                AdjustmentValueDic[key].Init();
            }
        }

        public void StartAdjustment()
        {
            for (int i = 0; i < adjustmentValuesByGameTime.Length; i++)
            {
                adjustmentValuesByGameTime[i].Start();
            }
        }
        void Update()
        {
            for (int i = 0; i < adjustmentValuesByGameTime.Length; i++)
            {
                adjustmentValuesByGameTime[i].Update();
            }
        }

        public float GetFloat(AdjustmentValueName name)
        {
            return GetValue<float>(name);
        }

        public float GetInt(AdjustmentValueName name)
        {
            return GetValue<int>(name);
        }

        T GetValue<T>(AdjustmentValueName name)
        {
            IAdjustmentValue value;

            if (!AdjustmentValueDic.TryGetValue(name, out value)) return default(T);

            return ((AbstractAdjustmentValue<T>)value).CurrentValue;
        }
    }
}
