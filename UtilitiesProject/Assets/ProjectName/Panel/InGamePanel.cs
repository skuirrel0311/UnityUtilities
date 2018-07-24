using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class InGamePanel : GameStatePanel
{
    protected override EventType[] ActivateStates { get { return new[] { EventType.GameStart }; } }
    protected override EventType[] DeactivateStates { get { return new[] { EventType.Initialize }; } }
}
