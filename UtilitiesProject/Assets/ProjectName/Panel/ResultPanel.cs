using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class ResultPanel : GameStatePanel
{
    protected override GameState ActivateState { get { return GameState.Result; } }
    protected override GameState DeactivateState { get { return GameState.Ready; } }
}
