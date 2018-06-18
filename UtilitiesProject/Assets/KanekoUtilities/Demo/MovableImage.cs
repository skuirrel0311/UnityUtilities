using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MovableImage : UGUIImage
{
    [SerializeField]
    float endX = 360.0f;

    public EaseType ease = EaseType.InQuad;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Vector2 startPosition = Image.rectTransform.anchoredPosition;
            Vector2 targetPosition = startPosition;
            targetPosition.x = endX;
            
            StartCoroutine(KKUtilities.FloatLerp(1.0f, (t) =>
            {
                Image.rectTransform.anchoredPosition = Vector2.LerpUnclamped(startPosition, targetPosition, GetEase(t));
            }));
        }
    }

    float GetEase(float t)
    {
        switch (ease)
        {
            case EaseType.InQuad: return Easing.InQuad(t);
            case EaseType.InCubic: return Easing.InCubic(t);
            case EaseType.InQuart: return Easing.InQuart(t);
            case EaseType.InQuint: return Easing.InQuint(t);
            case EaseType.InExpo: return Easing.InExpo(t);
            case EaseType.InBack: return Easing.InBack(t);

            case EaseType.OutQuad: return Easing.OutQuad(t);
            case EaseType.OutCubic: return Easing.OutCubic(t);
            case EaseType.OutQuart: return Easing.OutQuart(t);
            case EaseType.OutQuint: return Easing.OutQuint(t);
            case EaseType.OutExpo: return Easing.OutExpo(t);
            case EaseType.OutBack: return Easing.OutBack(t);

            case EaseType.InOutQuad: return Easing.InOutQuad(t);
            case EaseType.InOutCubic: return Easing.InOutCubic(t);
            case EaseType.InOutQuart: return Easing.InOutQuart(t);
            case EaseType.InOutQuint: return Easing.InOutQuint(t);
            case EaseType.InOutExpo: return Easing.InOutExpo(t);
            case EaseType.InOutBack: return Easing.InOutBack(t);
        }

        return 1.0f;
    }
}
