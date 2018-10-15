using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyGameManager : BaseGameManager<MyGameManager>
{
#if INFINITE_MODE
    protected override IEnumerator OneGame()
    {
        Init();

        yield return StartCoroutine(SuggestStart());
        GameStart();

        while (true)
        {
            while (!IsGameOver())
            {
                yield return null;
            }
            GameOver();
            if (!CanContinue) break;
            yield return StartCoroutine(SuggestContinue());

            if (isContinueRequested == ContinueRequestType.NoThanks) yield break; ;
            if (isContinueRequested == ContinueRequestType.TimeOut) break;
            Continue();

            if (isFailedContinue) break;
        }
        yield return StartCoroutine(SuggestRestart());
    }
#endif

#if STAGE_MODE
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

            if (isFailedContinue) break;
        }

        if (isStageClear)
        {
            StageClear();
        }
        yield return StartCoroutine(SuggestRestart());
    }
#endif

#if !STAGE_MODE && !INFINITE_MODE
    protected override IEnumerator OneGame()
    {
        while(true)
        {
            yield return null;
        }
    }
#endif
}
