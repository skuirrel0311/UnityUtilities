using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class NotiifcationMark : MonoBehaviour
{
    [SerializeField]
    UGUIImage baseImage = null;

    [SerializeField]
    UGUIImage markImage = null;

    [SerializeField]
    AnimationCurve baseImageScaleCurve = null;
    [SerializeField]
    AnimationCurve markImageScaleCurve = null;
    [SerializeField]
    AnimationCurve markImageRotCurve = null;

    System.Func<bool> condition;
    bool isAnimating;

    public void SetCondition(System.Func<bool> condition)
    {
        this.condition = condition;
    }

    void OnEnable()
    {
        if (!isAnimating) return;

        isAnimating = false;
        Show();
    }

    public void Show()
    {
        if (!IsShow())
        {
            Hide();
            return;
        }

        if (isAnimating) return;
        baseImage.gameObject.SetActive(true);
        baseImage.transform.localScale = Vector3.one;
        markImage.transform.localScale = Vector3.one;
        isAnimating = true;
            
        StartCoroutine(Animation());
    }

    bool IsShow()
    {
        if (condition == null) return true;
        return condition.Invoke();
    }

    public void Hide()
    {
        StopAllCoroutines();
        baseImage.gameObject.SetActive(false);
        isAnimating = false;
    }

    IEnumerator Animation()
    {
        while (true)
        {
            yield return KKUtilities.FloatLerp(2.0f, (t) =>
            {
                //baseImage.transform.localScale = Vector3.one * Mathf.LerpUnclamped(1.0f, 1.3f, baseImageScaleCurve.Evaluate(t));
                markImage.transform.localScale = Vector3.one * Mathf.LerpUnclamped(1.0f, 1.5f, markImageScaleCurve.Evaluate(t));
                baseImage.transform.SetLocalRotationZ(Mathf.LerpUnclamped(0.0f, 30.0f, markImageRotCurve.Evaluate(t)));
            });
        }
    }
}
