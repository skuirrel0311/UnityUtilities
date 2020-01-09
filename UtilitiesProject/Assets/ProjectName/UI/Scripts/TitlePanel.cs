using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TitlePanel : GameStatePanel
{
    [SerializeField]
    UGUIButton touchToStartButton = null;

    public IEnumerator SuggestGameStart()
    {
        yield return KKUtilities.WaitAction(touchToStartButton.OnClickEvent);
    }
}
