using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanekoUtilities;

public class DebugPanel : Panel
{
    [SerializeField]
    UGUIButton showButton = null;

    [SerializeField]
    UGUIButton hideButton = null;

    [SerializeField]
    InputField levelInputField = null;

    [SerializeField]
    Toggle hideUI = null;

    [SerializeField]
    CanvasGroup uiGroup = null;

    static float uiAlpha = 1.0f;

    protected override void Awake()
    {
        base.Awake();

        uiGroup.alpha = uiAlpha;
    }

    void Start()
    {
        showButton.AddListener(Activate);
        hideButton.AddListener(Deactivate);
    }

    public override void Activate()
    {
        levelInputField.text = MyGameManager.Instance.CurrentLevel.ToString();
        hideUI.isOn = uiAlpha <= 0.5f;
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();

        uiAlpha = hideUI.isOn ? 0.0f : 1.0f;

        int selectLevel;
        if (int.TryParse(levelInputField.text, out selectLevel))
        {
            MyGameManager.Instance.ChangeLevel(selectLevel);
        }
        else
        {
            MyGameManager.Instance.Retry();
        }
    }
}
