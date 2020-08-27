using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class ToggleSwitch : MonoBehaviour
    {
        [SerializeField]
        UGUIButton button = null;
        [SerializeField]
        bool isOn = true;
        public bool IsOn
        {
            get
            {
                return isOn;
            }
            set
            {
                if (isOn == value) return;
                isOn = value;
                OnValueChanged.SafeInvoke(isOn);
                if (!isInitialized) Initialize();
                ChangeStateAnimation();
            }
        }

        [Space]
        [SerializeField]
        UGUIImage circle = null;
        [SerializeField]
        Color unSelectedCircleColor = Color.red;
        [SerializeField]
        RectTransform unselectCirclePositionTransform = null;

        [SerializeField]
        UGUIImage bg = null;

        Color selectedCircleColor;
        Vector2 selectedCirclePosition;
        Vector2 unSelectedCirclePosition;

        public MyUnityEvent<bool> OnValueChanged = new MyUnityEvent<bool>();

        bool isInitialized;

        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            if (isInitialized) return;

            isInitialized = true;

            selectedCircleColor = bg.Color;
            selectedCirclePosition = circle.RectTransform.anchoredPosition;
            unSelectedCirclePosition = unselectCirclePositionTransform.anchoredPosition;

            button.AddListener(() =>
            {
                IsOn = !IsOn;
            });

            Refresh();
        }

        void ChangeStateAnimation()
        {
            StopAllCoroutines();
            var start = circle.RectTransform.anchoredPosition;
            var end = IsOn ? selectedCirclePosition : unSelectedCirclePosition;

            if (!gameObject.activeInHierarchy)
            {
                Refresh();
                return;
            }

            StartCoroutine(KKUtilities.FloatLerp(0.15f, (t) =>
            {
                circle.RectTransform.anchoredPosition = Vector2.Lerp(start, end, Easing.OutQuad(t));
            }).OnCompleted(() =>
            {
                Refresh();
            }));
        }

        void Refresh()
        {
            bg.Color = IsOn ? selectedCircleColor : unSelectedCircleColor;
            circle.RectTransform.anchoredPosition = IsOn ? selectedCirclePosition : unSelectedCirclePosition;
        }
    }
}
