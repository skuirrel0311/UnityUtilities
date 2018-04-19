using UnityEngine;

public interface IAdjustmentValue
{
    AdjustmentValueName Name { get; }
    void Init();
    void LevelUp();
}

public abstract class AbstractAdjustmentValue<T> : IAdjustmentValue
{
    [SerializeField]
    AdjustmentValueName name = 0;

    [SerializeField]
    protected T firstValue;
    [SerializeField]
    protected T limitValue;
    
    //レベルアップするたびにどれだけ変化するかの倍率
    [SerializeField]
    protected float magnification = 1.1f;

    public AdjustmentValueName Name { get { return name; } }
    public T CurrentValue { get; protected set; }

    public virtual void Init()
    {
        CurrentValue = firstValue;
    }

    public abstract void LevelUp();
}

//トリガーが発生した時にレベルアップする調整要素
[System.Serializable]
public class AdjustmentValueByTrigger : AbstractAdjustmentValue<int>
{
    [SerializeField]
    int levelupBoder;

    public override void LevelUp()
    {
        CurrentValue = (int)(CurrentValue * magnification);

        //上限値を超えていたら
        if (magnification > 1 == CurrentValue >= limitValue)
        {
            CurrentValue = limitValue;
        }
    }
}

[System.Serializable]
public class AdjustmentValueByGameTime : AbstractAdjustmentValue<float>
{
    [SerializeField]
    bool autoStart = true;

    [SerializeField]
    float levelupIntervalTime = 1.0f;
    
    float elapsedTime = 0.0f;
    bool canLevelUp = false;

    public override void Init()
    {
        base.Init();

        if (autoStart) Start();
    }

    public void Start()
    {
        canLevelUp = true;
    }

    public void Update()
    {
        if (!canLevelUp) return;

        elapsedTime += Time.deltaTime;

        if(elapsedTime > levelupIntervalTime)
        {
            elapsedTime = 0.0f;
            LevelUp();
        }
    }

    public override void LevelUp()
    {
        CurrentValue *= magnification;

        //上限値を超えていたら
        if (magnification > 1 == CurrentValue >= limitValue)
        {
            Stop();
        }
    }

    void Stop()
    {
        canLevelUp = false;
        CurrentValue = limitValue;
    }
}