using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StageGenerator))]
[RequireComponent(typeof(StageColorCoordinator))]
public class StageFacade : MonoBehaviour
{
    public StageGenerator Generator { get; private set; }
    public StageColorCoordinator ColorCoordinator { get; private set; }

    void Awake()
    {
        Generator = GetComponent<StageGenerator>();
        ColorCoordinator = GetComponent<StageColorCoordinator>();
    }

    public void Init()
    {
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
