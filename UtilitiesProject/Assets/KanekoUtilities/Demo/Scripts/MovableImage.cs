using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;
using DG.Tweening;

public class MovableImage : UGUIImage
{
    [SerializeField]
    float endX = 360.0f;

    public EaseType ease = EaseType.InQuad;

    //void Start()
    //{
    //    Sequence sequence = DOTween.Sequence();

    //    sequence.Append(transform.DOMoveX(40.0f, 3.0f).SetEase(Ease.InQuad));
    //    sequence.Join(transform.DOMoveY(40.0f, 3.0f));
    //}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Vector2 startPosition = Image.rectTransform.anchoredPosition;
            Vector2 targetPosition = startPosition;
            targetPosition.x = endX;
            
            StartCoroutine(KKUtilities.FloatLerp(3.0f, (t) =>
            {
                Image.rectTransform.anchoredPosition = Vector2.LerpUnclamped(startPosition, targetPosition, Easing.GetEase(t, ease));
            }));
        }
    }
}
