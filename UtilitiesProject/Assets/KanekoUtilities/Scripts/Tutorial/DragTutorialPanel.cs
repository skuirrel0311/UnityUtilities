using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class DragTutorialPanel : TutorialPanel
{
    [SerializeField]
    UGUIImage startPoint = null;

    [SerializeField]
    UGUIImage endPoint = null;

    [SerializeField]
    float speed = 7.0f;

    protected override void Awake()
    {
        fingerRectTransform.gameObject.SetActive(false);
        startPoint.Alpha = 0.0f;
        endPoint.Alpha = 0.0f;

        base.Awake();
    }

    protected override IEnumerator TutorialAnimation()
    {
        var wait = new WaitForSeconds(0.5f);

        while (true)
        {
            fingerRectTransform.anchoredPosition = startPoint.RectTransform.anchoredPosition;
            ChangeImage(FingerType.Up);
            fingerRectTransform.gameObject.SetActive(true);

            yield return wait;

            ChangeImage(FingerType.Down);

            yield return wait;

            var start = startPoint.RectTransform.anchoredPosition;
            var end = endPoint.RectTransform.anchoredPosition;
            var duration = KKUtilities.GetDuration(start, end, speed);

            yield return KKUtilities.FloatLerp(duration, (t) =>
            {
                end = endPoint.RectTransform.anchoredPosition;
                fingerRectTransform.anchoredPosition = Vector3.Lerp(start, end, t);
            }, false);

            ChangeImage(FingerType.Up);

            yield return wait;
            fingerRectTransform.gameObject.SetActive(false);
        }
    }
}
