using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MovableImage : UGUIImage
{
    [SerializeField]
    float endX = 360.0f;

    public Ease ease = Ease.InQuad;


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
            case Ease.InQuad: return Easing.InQuad(t);
            case Ease.InCubic: return Easing.InCubic(t);
            case Ease.InQuart: return Easing.InQuart(t);
            case Ease.InQuint: return Easing.InQuint(t);
            case Ease.InExpo: return Easing.InExpo(t);
            case Ease.InBack: return Easing.InBack(t);

            case Ease.OutQuad: return Easing.OutQuad(t);
            case Ease.OutCubic: return Easing.OutCubic(t);
            case Ease.OutQuart: return Easing.OutQuart(t);
            case Ease.OutQuint: return Easing.OutQuint(t);
            case Ease.OutExpo: return Easing.OutExpo(t);
            case Ease.OutBack: return Easing.OutBack(t);

            case Ease.InOutQuad: return Easing.InOutQuad(t);
            case Ease.InOutCubic: return Easing.InOutCubic(t);
            case Ease.InOutQuart: return Easing.InOutQuart(t);
            case Ease.InOutQuint: return Easing.InOutQuint(t);
            case Ease.InOutExpo: return Easing.InOutExpo(t);
            case Ease.InOutBack: return Easing.InOutBack(t);
        }

        return 1.0f;
    }
}
