using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Button),typeof(CanvasGroup))]
    public class UGUIButton : UGUIParts
    {
        protected Button button;
        public Button Button
        {
            get
            {
                if(button == null)
                {
                    button = GetComponent<Button>();
                }

                return button;
            }
        }

        [SerializeField]
        CanvasGroup group = null;

        public bool Interactable
        {
            get
            {
                return Button.interactable;
            }
            set
            {
                Button.interactable = value;
            }
        }

        public UnityEvent OnClickEvent = new UnityEvent();

        public void AddListener(UnityAction action)
        {
            OnClickEvent.AddListener(action);
        }

        protected virtual void OnClick()
        {
            OnClickEvent.Invoke();
        }

        public override Color Color
        {
            get
            {
                return Button.targetGraphic.color;
            }
            set
            {
                if(Color == value) return;
                Button.image.color = value;
            }
        }

        public override float Alpha
        {
            get
            {
                return group.alpha;
            }

            set
            {
                group.alpha = value;
            }
        }

        protected virtual void Awake()
        {
            Interactable = Button.interactable;

            button.onClick.AddListener(OnClick);
        }
    }
}