using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class SuggestWindow : Panel
    {
        [SerializeField]
        UGUIButton okButton = null;

        [SerializeField]
        UGUIButton noThanksButton = null;

        public MyUnityEvent OnOK = new MyUnityEvent();
        public MyUnityEvent OnNoThanks = new MyUnityEvent();

        protected override void Awake()
        {
            okButton.AddListener(() => OnOK.SafeInvoke());
            noThanksButton.AddListener(() => OnNoThanks.SafeInvoke());
            base.Awake();
        }

        public override void Activate()
        {
            okButton.gameObject.SetActive(false);
            noThanksButton.gameObject.SetActive(false);
            base.Activate();
            StartCoroutine(Show());
        }

        public virtual IEnumerator Show()
        {
            yield return okButton.PopUp(0.3f);

            noThanksButton.Alpha = 0.0f;
            noThanksButton.gameObject.SetActive(true);
            noThanksButton.Fade(1.0f, 0.5f);
        }
    }
}