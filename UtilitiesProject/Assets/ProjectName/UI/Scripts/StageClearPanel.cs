using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class StageClearPanel : GameStatePanel
{
    [SerializeField]
    GameObject tapToRestartContainer = null;
    [SerializeField]
    UGUIButton tapToRestartButton = null;

    public IEnumerator SuggestRestart()
    {
        tapToRestartContainer.SetActive(true);

        yield return KKUtilities.WaitAction(tapToRestartButton.OnClickEvent, ()=>
        {
            tapToRestartContainer.SetActive(false);
        });
    }
}
