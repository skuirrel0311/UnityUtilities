using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public enum PanelType
{
    None, Title, InGame, GameOver, StageClear
}

public class GameStatePanel : Panel
{
    [SerializeField]
    PanelType type = 0;
    public PanelType Type { get { return type; } }
}
