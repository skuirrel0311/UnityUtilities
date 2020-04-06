using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TitlePanel : GameStatePanel
{
    [SerializeField]
    UGUIButton touchToStartButton = null;

    [SerializeField]
    AbstractUGUIText currentLevelText = null;

    public override void Activate()
    {
        currentLevelText.Text = "level " + MyGameManager.Instance.CurrentLevel;
        base.Activate();
    }

    public IEnumerator SuggestGameStart()
    {
        yield return KKUtilities.WaitAction(touchToStartButton.OnClickEvent);
    }
}
