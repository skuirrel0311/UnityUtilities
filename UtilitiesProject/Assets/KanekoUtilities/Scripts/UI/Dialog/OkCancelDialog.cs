using System;
using UnityEngine;
using UnityEngine.Events;

namespace KanekoUtilities
{
    public class OkCancelDialog : Dialog
    {
        [SerializeField]
        UGUIButton okButton = null;
        [SerializeField]
        UGUIButton cancelButton = null;

        [SerializeField]
        bool autoHide = true;

        [SerializeField]
        AbstractUGUIText okButtonMessage = null;
        public AbstractUGUIText OkButtonMessage { get { return okButtonMessage; } }
        [SerializeField]
        AbstractUGUIText cancelButtonMessage = null;
        public AbstractUGUIText CancelButtonMessage { get { return cancelButtonMessage; } }
        [SerializeField]
        AbstractUGUIText message = null;
        public AbstractUGUIText Message { get { return message; } }

        public void Init(Action onOk, Action onCancel)
        {
            okButton.OnClick.RemoveAllListeners();
            cancelButton.OnClick.RemoveAllListeners();

            okButton.OnClick.AddListener(()=>
            {
                if(onOk != null) onOk.Invoke();
                if (autoHide) DialogDisplayer.Instance.HideDialog();
            });
            cancelButton.OnClick.AddListener(() =>
            {
                if (onCancel != null) onCancel.Invoke();
                if (autoHide) DialogDisplayer.Instance.HideDialog();
            });
        }
    }
}
