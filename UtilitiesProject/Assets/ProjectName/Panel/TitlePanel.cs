using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;
using System;

public class TitlePanel : GameStatePanel
{
    protected override GameState ActivateState { get { return GameState.Ready; } }
    protected override GameState DeactivateState { get { return GameState.InGame; } }
}
