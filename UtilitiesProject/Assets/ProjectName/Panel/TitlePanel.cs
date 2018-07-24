using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;
using System;

public class TitlePanel : GameStatePanel
{
    protected override EventType[] ActivateStates { get { return new[] { EventType.Initialize }; } }
    protected override EventType[] DeactivateStates { get { return new[] { EventType.GameStart }; } }
}
