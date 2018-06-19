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
        
        protected override void Start()
        {
            base.Start();

            if (autoHide)
            {
                OnClick.AddListener(() =>
                {
                    Hide();
                });
            }
        }
    }
}