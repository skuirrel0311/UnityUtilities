﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(MessageTextPool))]
    public class MessageDisplayer : SingletonMonobehaviour<MessageDisplayer>
    {
        [Serializable]
        public struct FontSettings
        {
            public Color color;
            public int fontSize;
            public TextAnchor anchor;

            public FontSettings(Text text)
            {
                color = text.color;
                fontSize = text.fontSize;
                anchor = text.alignment;
            }
        }

        ObjectPool<UGUIText> textPooler = null;

        public FontSettings DefaultFontSettings { get; private set; }

        [SerializeField]
        Camera uiCamera = null;

        protected override void Awake()
        {
            base.Awake();

            textPooler = GetComponent<ObjectPool<UGUIText>>();
            Text text = textPooler.GetInstance().Message;
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
            Text message = textPooler.GetInstance().Message;
            message.text = text;

            message.color = settings.color;
            message.fontSize = settings.fontSize;
            message.alignment = settings.anchor;
            message.transform.position = uiCamera.WorldToScreenPoint(position);
            message.gameObject.SetActive(true);

            KKUtilities.Delay(limitLife, () =>
            {
                message.gameObject.SetActive(false);
            }, this);
        }

        public void ShowMessage(string text, Vector2 position, FontSettings settings, float limitLife = 2.0f)
        {
            Text message = textPooler.GetInstance().Message;

            message.text = text;

            message.color = settings.color;
            message.fontSize = settings.fontSize;
            message.alignment = settings.anchor;
            message.rectTransform.anchoredPosition = position;
            message.gameObject.SetActive(true);

            if (limitLife <= 0.0f) return;
            KKUtilities.Delay(limitLife, () =>
            {
                message.gameObject.SetActive(false);
            }, this);
        }

        public void ShowMessage(string text, Vector2 position, Action<float, Text> onUpdate, FontSettings settings, float limitLife = 2.0f)
        {
            Text message = textPooler.GetInstance().Message;

            message.text = text;

            message.color = settings.color;
            message.fontSize = settings.fontSize;
            message.alignment = settings.anchor;
            message.rectTransform.anchoredPosition = position;
            message.gameObject.SetActive(true);

            if (limitLife <= 0.0f) return;

            StartCoroutine(KKUtilities.FloatLerp(limitLife, (t) => onUpdate.Invoke(t, message)).OnCompleted(() => message.gameObject.SetActive(false)));
        }

        public void ShowMessage(string text, Vector2 position, Action<float, Text> onUpdate, float limitLife = 2.0f)
        {
            ShowMessage(text, position, onUpdate, DefaultFontSettings, limitLife);
        }
    }
}
