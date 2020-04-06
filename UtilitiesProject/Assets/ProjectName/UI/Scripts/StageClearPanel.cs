using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class StageClearPanel : GameStatePanel
{
    [SerializeField]
    UGUIButton nextButton = null;

    [SerializeField]
    AbstractUGUIText message = null;

    public override void Activate()
    {
        message.Text = "level " + MyGameManager.Instance.CurrentLevel + "\n completed!!";
        base.Activate();
    }

    public IEnumerator SuggestRestart()
    {
        yield return KKUtilities.WaitAction(nextButton.OnClickEvent);
    }
}
