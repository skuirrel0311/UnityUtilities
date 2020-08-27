using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class LanguageTranslater : SingletonMonobehaviour<LanguageTranslater>
{
    public enum WordId
    {
        Settings, Vibration, Sound,
    }

    class WordSet
    {
        public string English;
        public string Chinese;
        public string Japanese;

        public WordSet(string english, string chinese, string japanese)
        {
            English = english;
            Japanese = japanese;
            Chinese = chinese;
        }

        public string GetWord(SystemLanguage lang)
        {
            switch (lang)
            {
                case SystemLanguage.English: return English;
                case SystemLanguage.ChineseSimplified: return Chinese;
                case SystemLanguage.Japanese: return Japanese;
            }

            return English;
        }
    }

    Dictionary<WordId, WordSet> wordLibrary = new Dictionary<WordId, WordSet>();
    bool isInitialized;

    [SerializeField]
    Font defaultFont = null;

    [SerializeField]
    Font chineseFont = null;

    [SerializeField]
    Font japaneseFont = null;
    
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        if (isInitialized) return;
        isInitialized = true;
        wordLibrary.Add(WordId.Settings, new WordSet("SETTINGS", "设置", "設定"));
        wordLibrary.Add(WordId.Vibration, new WordSet("Vibration", "振动", "振動"));
        wordLibrary.Add(WordId.Sound, new WordSet("Sound", "音效", "サウンド"));
    }

    public string GetText(WordId id)
    {
        var word = GetWord(id);
        if (word == null) return "";
        return word.GetWord(GetLang());
    }

    public string GetText(WordId id, SystemLanguage lang)
    {
        var word = GetWord(id);
        if (word == null) return "";
        return word.GetWord(lang);
    }

    WordSet GetWord(WordId id)
    {
        if (!isInitialized) Init();
        return wordLibrary.SafeGetValue(id);
    }

    public Font GetFont()
    {
        var systemLang = GetLang();

        if (systemLang == SystemLanguage.ChineseSimplified) return chineseFont;
        if (systemLang == SystemLanguage.Japanese) return japaneseFont;

        return defaultFont;
    }

    public SystemLanguage GetLang()
    {
        var systemLang = Application.systemLanguage;

        if (systemLang == SystemLanguage.ChineseSimplified ||
            systemLang == SystemLanguage.Japanese)
        {
            return systemLang;
        }

        return SystemLanguage.English;
    }
}
