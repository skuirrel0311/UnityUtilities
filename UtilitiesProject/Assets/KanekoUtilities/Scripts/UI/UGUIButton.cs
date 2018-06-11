using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Button))]
    public class UGUIButton : UGUIParts
    {
        Button button;
        public Button Button
        {
            get
            {
                if (button == null)
                {
                    button = GetComponent<Button>();
                }
                return button;
            }
        }

        public UnityEvent OnClick
        {
            get
            {
                return Button.onClick;
            }
        }

        public override Color Color
        {
            get
            {
                return Button.image.color;
            }
            set
            {
                if (Color == value) return;
                Button.image.color = value;
            }
        }
    }
}