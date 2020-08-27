using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanekoUtilities;

public class MultLanguageText : MonoBehaviour
{
    [SerializeField]
    LanguageTranslater.WordId wordId = 0;

    [SerializeField]
    SystemLanguage debugLang;

    Text text;

#if UNITY_EDITOR
    void OnValidate()
    {
        //if (Application.isPlaying) return;

        SetText(LanguageTranslater.Instance.GetText(wordId, debugLang));
    }
#endif

    void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        SetText(LanguageTranslater.Instance.GetText(wordId));
        text.font = LanguageTranslater.Instance.GetFont();
    }

    void SetText(string message)
    {
        if (text == null) text = GetComponent<Text>();

        if (text == null) return;
        text.text = message;
    }
}
