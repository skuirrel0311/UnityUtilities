using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageModeGameManager : BaseGameManager<StageModeGameManager>
{
    protected override GameMode Mode { get { return GameMode.Stage; } }

    protected override IEnumerator OneGame()
    {
        Init();
        yield return StartCoroutine(SuggestStart());
        GameStart();
        bool isStageClear = false;
        
        while (true)
        {
            while (!IsGameOver())
            {
                yield return null;
                if (IsStageClear())
                {
                    isStageClear = true;
                    break;
                }
            }
            
            if (isStageClear) break;
            GameOver();
            if (!CanContinue) break;
            yield return StartCoroutine(SuggestContinue());

            if (isContinueRequested == ContinueRequestType.NoThanks) yield break;
            if (isContinueRequested == ContinueRequestType.TimeOut) break;
            
            Continue();
        }

        if (isStageClear)
        {
            StageClear();
        }
        yield return StartCoroutine(SuggestRestart());
    }
}
