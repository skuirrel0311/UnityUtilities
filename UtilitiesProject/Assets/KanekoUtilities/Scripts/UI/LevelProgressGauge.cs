using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class LevelProgressGauge : MonoBehaviour
{
    [SerializeField]
    GameObject elementOriginal = null;

    [SerializeField]
    GameObject bonusStageIcon = null;

    [SerializeField]
    Transform elementContainer = null;
    
    CanvasGroup group;
    GameObject[] elements;

    bool isInitialized;

    void Awake()
    {
        group = GetComponent<CanvasGroup>();
        
        Init();
    }

    public void Init()
    {
        if (isInitialized) return;
        isInitialized = true;
        
        elements = new GameObject[3];
        for (int i = 0; i < elements.Length; i++)
        {
            var temp = Instantiate(elementOriginal, elementContainer);
            temp.SetActive(true);
            elements[i] = temp.transform.GetChild(0).gameObject;
        }

        bonusStageIcon.transform.SetAsLastSibling();
        bonusStageIcon.SetActive(true);
        bonusStageIcon.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Activate()
    {
        group.alpha = 1.0f;
    }

    public void Deactivate()
    {
        group.alpha = 0.0f;
    }

    public void Refresh(int currentStageIndex)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].SetActive(currentStageIndex > i);
        }

        var isClearBonusStage = currentStageIndex > elements.Length;
        bonusStageIcon.transform.GetChild(1).gameObject.SetActive(isClearBonusStage);
    }
}
