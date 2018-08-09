using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TitlePanel : Panel
{
    [SerializeField]
    UGUIButton touchToStartButton = null;

    public IEnumerator SuggestGameStart()
    {
        yield return KKUtilities.WaitAction(touchToStartButton.OnClick);
    }
}
