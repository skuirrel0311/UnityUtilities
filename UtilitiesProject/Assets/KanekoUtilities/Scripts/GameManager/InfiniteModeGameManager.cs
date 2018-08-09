using System.Collections;

public partial class InfiniteModeGameManager : BaseGameManager<InfiniteModeGameManager>
{
    protected override GameMode Mode { get { return GameMode.Infinite; } }

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
}
