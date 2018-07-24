using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class StageClearPanel : GameStatePanel
{
    protected override EventType[] ActivateStates { get { return new[] { EventType.StageClear }; } }
    protected override EventType[] DeactivateStates { get { return new[] { EventType.Initialize }; } }

    [SerializeField]
    GameObject tapToRestartContainer = null;
    [SerializeField]
    UGUIButton tapToRestartButton = null;

    public IEnumerator SuggestRestart()
    {
        tapToRestartContainer.SetActive(true);

        yield return KKUtilities.WaitAction(tapToRestartButton.OnClick, ()=>
        {
            tapToRestartContainer.SetActive(false);
        });
    }
}
