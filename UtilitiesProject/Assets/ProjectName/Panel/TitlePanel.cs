using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TitlePanel : GameStatePanel
{
    [SerializeField]
    UGUIButton touchToStartButton = null;

    protected override EventType[] ActivateStates { get { return new[] { EventType.Initialize }; } }
    protected override EventType[] DeactivateStates { get { return new[] { EventType.GameStart }; } }
    
    public IEnumerator SuggestGameStart()
    {
        yield return KKUtilities.WaitAction(touchToStartButton.OnClick);
    }
}
