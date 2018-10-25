using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerEffector))]
[RequireComponent(typeof(PlayerVisualChanger))]
public class PlayerFacade : ActorFacade
{
    public PlayerController Controller { get; private set; }
    public PlayerPhysics Physics { get; private set; }
    public PlayerEffector Effector { get; private set; }
    public PlayerVisualChanger VisualChanger { get; private set; }

    void Awake()
    {
        Controller = GetComponent<PlayerController>();
        Physics = GetComponent<PlayerPhysics>();
        Effector = GetComponent<PlayerEffector>();
        VisualChanger = GetComponent<PlayerVisualChanger>();

        Controller.SetOwner(this);
        Physics.SetOwner(this);
        Effector.SetOwner(this);
        VisualChanger.SetOwner(this);
    }

    public void Init()
    {
        VisualChanger.Init();
        Physics.Init();
        Controller.Init();
        Effector.Init();
    }

    
    public void OnGameStart()
    {
        Controller.CanMove = true;
    }

    
    //GameOver or StageClear
    public void OnGameEnd()
    {
        Controller.CanMove = false;
    }
}
