using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KanekoUtilities
{
    public class OKDialog : Dialog
    {
        [SerializeField]
        UGUIButton okButton = null;
        [SerializeField]
        bool autoHide = true;

        public UnityEvent OnClick
        {
            get
            {
                return okButton.OnClick;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            OnClick.AddListener(OnOK);
        }

        protected virtual void OnOK()
        {
            if (autoHide) Hide();
        }
    }
}