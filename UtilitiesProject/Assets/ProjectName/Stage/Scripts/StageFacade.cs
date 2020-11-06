using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFacade : MonoBehaviour
{
    [SerializeField]
    StageData[] stages = null;

    [System.NonSerialized]
    public StageData CurrentStageData;

    public void Init()
    {
        if (stages == null || stages.Length == 0) return;

        foreach (var s in stages) s.gameObject.SetActive(false);

        CurrentStageData = stages[LevelToStageDataIndex(MyGameManager.Instance.CurrentLevel)];
        CurrentStageData.gameObject.SetActive(true);
        CurrentStageData.Init();
    }

    public void OnGameStart()
    {

    }

    //GameOver or StageClear
    public void OnGameEnd()
    {

    }

    int LevelToStageDataIndex(int level)
    {
        if (level <= 1) return 0;

        var stageLevel = level;
        var max = stages.Length;
        //for (int i = 0; i < packs.Length; i++) max += packs[i].Count;

        if (stageLevel <= max) return stageLevel - 1;

        var tutorialStageNum = 1;
        var temp = (stageLevel - max) % (max - tutorialStageNum);
        if (temp == 0) temp = max - tutorialStageNum;
        stageLevel = (temp) + tutorialStageNum;
        return stageLevel - 1;
    }
}
