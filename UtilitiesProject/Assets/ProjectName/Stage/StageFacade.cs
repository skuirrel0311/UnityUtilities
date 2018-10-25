using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StageGenerator))]
[RequireComponent(typeof(StageCleaner))]
[RequireComponent(typeof(StageColorCoordinator))]
public class StageFacade : MonoBehaviour
{
    public StageGenerator Generator { get; private set; }
    public StageCleaner Cleaner { get; private set; }
    public StageColorCoordinator ColorCoordinator { get; private set; }

    void Awake()
    {
        Generator = GetComponent<StageGenerator>();
        Cleaner = GetComponent<StageCleaner>();
        ColorCoordinator = GetComponent<StageColorCoordinator>();
    }

    public void Init()
    {
        Cleaner.Init();
        Generator.Init();
        ColorCoordinator.Init();
    }

    public void OnGameStart()
    {

    }

    //GameOver or StageClear
    public void OnGameEnd()
    {

    }
}
