using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class PanelSwitcher : MonoBehaviour
{
    Dictionary<PanelType, GameStatePanel> panelDic = new Dictionary<PanelType, GameStatePanel>();

    List<PanelType> currentViewPanelTypeList = new List<PanelType>();

    void Awake()
    {
        GameStatePanel[] panels = GetComponentsInChildren<GameStatePanel>();
        foreach(var p in panels)
        {
            panelDic.Add(p.Type, p);
        }
    }

    /// <summary>
    /// 今表示しているパネルをすべて隠して指定されたパネルを表示する
    /// </summary>
    public void SwitchPanel(PanelType type)
    {
        HideAll();
        AddPanel(type);
    }

    /// <summary>
    /// パネルがすでに表示されていてもそのパネルを隠さず、指定されたパネルを表示する
    /// </summary>
    public void AddPanel(PanelType type)
    {
        GameStatePanel panel = GetPanel(type);

        if (panel == null) return;

        panel.Activate();
        currentViewPanelTypeList.Add(type);
    }

    /// <summary>
    /// 指定されたパネルを隠す
    /// </summary>
    public void HidePanel(PanelType type)
    {
        GameStatePanel panel;
        panel = GetPanel(type);

        if (panel == null) return;

        panel.Deactivate();
    }

    /// <summary>
    /// 全てのパネルを隠す
    /// </summary>
    public void HideAll()
    {
        if (currentViewPanelTypeList.Count <= 0) return;

        foreach (var t in currentViewPanelTypeList)
        {
            HidePanel(t);
        }

        currentViewPanelTypeList.Clear();
    }

    /// <summary>
    /// 指定されたパネルを取得する
    /// </summary>
    public GameStatePanel GetPanel(PanelType type)
    {
        GameStatePanel panel;
        if (panelDic.TryGetValue(type, out panel))
        {
            return panel;
        }

        return null;
    }
}
