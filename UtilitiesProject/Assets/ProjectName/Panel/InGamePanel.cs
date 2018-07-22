using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class InGamePanel : GameStatePanel
{
    protected override GameState ActivateState { get { return GameState.InGame; } }
    protected override GameState DeactivateState { get { return GameState.Ready; } }
}
