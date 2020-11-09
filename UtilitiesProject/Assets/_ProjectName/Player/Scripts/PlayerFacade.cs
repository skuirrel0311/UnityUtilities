using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class PlayerFacade : MonoBehaviour
{
    public void Init()
    {
    }

    
    public void OnGameStart()
    {
    }

    
    //GameOver or StageClear
    public void OnGameEnd()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MyGameManager.Instance.OnGameOver();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            MyGameManager.Instance.OnStageClear();
        }
    }
}
