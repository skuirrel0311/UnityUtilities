using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class TapTutorialPanel : TutorialPanel
    {
        [SerializeField]
        float waitTime = 1.0f;

        protected override void Awake()
        {
            fingerRectTransform.gameObject.SetActive(false);

            base.Awake();
        }

        protected override IEnumerator TutorialAnimation()
        {
            WaitForSeconds wait = new WaitForSeconds(waitTime);
            fingerRectTransform.gameObject.SetActive(true);
            while (true)
            {
                ChangeImage(FingerType.Down);
                yield return wait;
                ChangeImage(FingerType.Up);
                yield return wait;
            }
        }
    }
}