using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MultiLanguageImage : MonoBehaviour
{
    [System.Serializable]
    class Element
    {
        public SystemLanguage Lang;
        public GameObject Image;
    }

    [SerializeField]
    Element[] elements = null;

    void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach(var e in elements)
        {
            e.Image.SetActive(LanguageTranslater.Instance.GetLang() == e.Lang);
        }
    }
}
