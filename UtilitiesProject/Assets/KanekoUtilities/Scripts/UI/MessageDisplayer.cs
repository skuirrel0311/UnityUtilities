using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(UGUITextPool))]
    public class MessageDisplayer : SingletonMonobehaviour<MessageDisplayer>
    {
        [Serializable]
        public struct FontSettings
        {
            public Color color;
            public int fontSize;
            public TextAnchor anchor;

            public FontSettings(UGUIText text)
            {
                color = text.Color;
                fontSize = text.FontSize;
                anchor = text.Alignment;
            }
        }
        
        UGUITextPool textPooler = null;

        public FontSettings DefaultFontSettings { get; private set; }

        [SerializeField]
        Camera uiCamera = null;

        protected override void Awake()
        {
            base.Awake();

            textPooler = GetComponent<UGUITextPool>();
            UGUIText text = textPooler.GetInstance();
            DefaultFontSettings = new FontSettings(text);
            text.gameObject.SetActive(false);
            if (uiCamera == null) uiCamera = Camera.main;
        }

        public void ShowMessage(string text, float limitLife = 2.0f)
        {
            ShowMessage(text, DefaultFontSettings, limitLife);
        }

        public void ShowMessage(string text, FontSettings settings, float limitLife = 2.0f)
        {
            ShowMessage(text, Vector2.zero, settings, limitLife);
        }

        public void ShowMessage(string text, Vector2 position, float limitLife = 2.0f)
        {
            ShowMessage(text, position, DefaultFontSettings, limitLife);
        }

        public void ShowMessage(string text, Vector3 position, float limitLife = 2.0f)
        {
            ShowMessage(text, position, DefaultFontSettings, limitLife);
        }

        public void ShowMessage(string text, Vector3 position, FontSettings settings, float limitLife = 2.0f)
        {
            UGUIText message = textPooler.GetInstance();
            message.Text = text;

            message.Color = settings.color;
            message.FontSize = settings.fontSize;
            message.Alignment = settings.anchor;
            message.transform.position = uiCamera.WorldToScreenPoint(position);
            message.gameObject.SetActive(true);

            KKUtilities.Delay(limitLife, () =>
            {
                message.gameObject.SetActive(false);
            }, this);
        }

        public void ShowMessage(string text, Vector2 position, FontSettings settings, float limitLife = 2.0f)
        {
            UGUIText message = textPooler.GetInstance();

            message.Text = text;

            message.Color = settings.color;
            message.FontSize = settings.fontSize;
            message.Alignment = settings.anchor;
            message.RectTransform.anchoredPosition = position;
            message.gameObject.SetActive(true);

            if (limitLife <= 0.0f) return;
            KKUtilities.Delay(limitLife, () =>
            {
                message.gameObject.SetActive(false);
            }, this);
        }

        public void ShowMessage(string text, Vector2 position, Action<float, UGUIText> onUpdate, FontSettings settings, float limitLife = 2.0f)
        {
            UGUIText message = textPooler.GetInstance();

            message.Text = text;

            message.Color = settings.color;
            message.FontSize = settings.fontSize;
            message.Alignment = settings.anchor;
            message.RectTransform.anchoredPosition = position;
            message.gameObject.SetActive(true);

            if (limitLife <= 0.0f) return;

            StartCoroutine(KKUtilities.FloatLerp(limitLife, (t) => onUpdate.Invoke(t, message)).OnCompleted(() => message.gameObject.SetActive(false)));
        }

        public void ShowMessage(string text, Vector2 position, Action<float, UGUIText> onUpdate, float limitLife = 2.0f)
        {
            ShowMessage(text, position, onUpdate, DefaultFontSettings, limitLife);
        }

        public void ShowMessage(string text, Vector2 position, float showAnimationTime, float hideAnimationTime)
        {
            ShowMessage(text, position,DefaultFontSettings, showAnimationTime, hideAnimationTime);
        }

        public void ShowMessage(string text, Vector2 position, FontSettings settings, float showAnimationTime, float hideAnimationTime)
        {
            UGUIText message = textPooler.GetInstance();
            message.Text = text;

            message.Color = settings.color;
            message.FontSize = settings.fontSize;
            message.Alignment = settings.anchor;
            message.RectTransform.anchoredPosition = position;
            message.gameObject.SetActive(true);

            StartCoroutine(KKUtilities.FloatLerp(showAnimationTime, (t) =>
            {
                DefaultShowLogic(message, t);
            }).OnCompleted(()=>
            {
                StartCoroutine(KKUtilities.FloatLerp(hideAnimationTime, (t) =>
                {
                    DefaultHideLogic(message, t);
                }));

            }));
        }

        readonly Vector3 TextStartScale = Vector3.one * 0.3f;
        readonly Vector3 OneVector = Vector3.one;
        public void DefaultShowLogic(UGUIText text, float t)
        {
            text.transform.localScale = Vector3.Lerp(TextStartScale, OneVector, Easing.OutQuad(t));
        }
        public void DefaultHideLogic(UGUIText text, float t)
        {
            text.Alpha = Mathf.Lerp(1.0f, 0.0f, Easing.InQuad(t));
        }
    }
}
