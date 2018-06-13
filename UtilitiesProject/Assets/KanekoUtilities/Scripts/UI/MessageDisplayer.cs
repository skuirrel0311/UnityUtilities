using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class MessageDisplayer : SingletonMonobehaviour<MessageDisplayer>
    {
        [Serializable]
        public struct FontSettings
        {
            public Color color;
            public int fontSize;
            public TextAnchor anchor;

            public FontSettings(IUIText text)
            {
                color = text.Color;
                fontSize = text.FontSize;
                anchor = text.Alignment;
            }
        }

        [SerializeField]
        UGUITextPool textPool = null;
        [SerializeField]
        TextMeshPool textMeshPool = null;

        public FontSettings DefaultUGUIFontSettings { get; private set; }
        public FontSettings Default3DFontSettings { get; private set; }

        [SerializeField]
        Camera uiCamera = null;

        protected override void Awake()
        {
            base.Awake();
            
            DefaultUGUIFontSettings = new FontSettings(textPool.GetOriginal);
            Default3DFontSettings = new FontSettings(textMeshPool.GetOriginal);

            if (uiCamera == null) uiCamera = Camera.main;
        }

        #region 引数違い
        public void ShowMessage(string message, Vector2 position, float limitLife = 2.0f)
        {
            ShowMessage(message, position, DefaultUGUIFontSettings, limitLife);
        }

        public void ShowMessage(string message, Vector3 position, float limitLife = 2.0f)
        {
            ShowMessage(message, position, DefaultUGUIFontSettings, limitLife);
        }

        public void PlayMessage(string message, Vector2 position, float showAnimationTime = 0.3f, float hideAnimationTime = 0.3f, float limitLife = 1.0f)
        {
            PlayMessage(message, position, DefaultUGUIFontSettings, showAnimationTime, hideAnimationTime, limitLife);
        }
        public void PlayMessage(string message, Vector2 position, FontSettings settings, float showAnimationTime = 0.3f, float hideAnimationTime = 0.3f, float limitLife = 1.0f)
        {
            PlayMessage(message, position, settings, UIAnimationUtil.DefaultShowAnimation, UIAnimationUtil.DefaultHideAnimation, showAnimationTime, hideAnimationTime, limitLife);
        }
        public void PlayMessage(string message, Vector2 position, UIAnimation showAnimation, UIAnimation hideAnimation, float showAnimationTime, float hideAnimationTime, float limitLife = 1.0f)
        {
            PlayMessage(message, position, DefaultUGUIFontSettings, showAnimation, hideAnimation, showAnimationTime, hideAnimationTime, limitLife);
        }

        public void PlayMessage(string message, Vector3 position, float showAnimationTime = 0.3f, float hideAnimationTime = 0.3f, float limitLife = 1.0f)
        {
            PlayMessage(message, position, DefaultUGUIFontSettings, showAnimationTime, hideAnimationTime, limitLife);
        }
        public void PlayMessage(string message, Vector3 position, FontSettings settings, float showAnimationTime = 0.3f, float hideAnimationTime = 0.3f, float limitLife = 1.0f)
        {
            PlayMessage(message, position, settings, UIAnimationUtil.DefaultShowAnimation, UIAnimationUtil.DefaultHideAnimation, showAnimationTime, hideAnimationTime, limitLife);
        }
        public void PlayMessage(string message, Vector3 position, UIAnimation showAnimation, UIAnimation hideAnimation, float showAnimationTime, float hideAnimationTime, float limitLife = 1.0f)
        {
            PlayMessage(message, position, DefaultUGUIFontSettings, showAnimation, hideAnimation, showAnimationTime, hideAnimationTime, limitLife);
        }

        public void ShowMessage3D(string message, Vector3 position, float limitLife = 2.0f)
        {
            ShowMessage3D(message, position, Default3DFontSettings, limitLife);
        }

        public void PlayMessage3D(string message, Vector3 position, float showAnimationTime = 0.3f, float hideAnimationTime = 0.3f, float limitLife = 1.0f)
        {
            PlayMessage3D(message, position, Default3DFontSettings, showAnimationTime, hideAnimationTime, limitLife);
        }
        public void PlayMessage3D(string message, Vector3 position, FontSettings settings, float showAnimationTime = 0.3f, float hideAnimationTime = 0.3f, float limitLife = 1.0f)
        {
            PlayMessage3D(message, position, settings, UIAnimationUtil.DefaultShowAnimation, UIAnimationUtil.DefaultHideAnimation, showAnimationTime, hideAnimationTime, limitLife);
        }
        public void PlayMessage3D(string message, Vector3 position, UIAnimation showAnimation, UIAnimation hideAnimation, float showAnimationTime, float hideAnimationTime, float limitLife = 1.0f)
        {
            PlayMessage3D(message, position, Default3DFontSettings, showAnimation, hideAnimation, showAnimationTime, hideAnimationTime, limitLife);
        }

        #endregion

        public void ShowMessage(string message, Vector2 position, FontSettings settings, float limitLife = 2.0f)
        {
            AbstractUGUIText text = textPool.GetInstance();
            text.RectTransform.anchoredPosition = position;

            InitializeUGUIText(text, message, settings);

            if (limitLife <= 0.0f) return;
            KKUtilities.Delay(limitLife, () =>
            {
                textPool.ReturnInstance(text);
            }, this);
        }
        public void ShowMessage(string message, Vector3 position, FontSettings settings, float limitLife = 2.0f)
        {
            AbstractUGUIText text = textPool.GetInstance();
            text.transform.position = uiCamera.WorldToScreenPoint(position);

            InitializeUGUIText(text, message, settings);
            text.gameObject.SetActive(true);

            KKUtilities.Delay(limitLife, () =>
            {
                textPool.ReturnInstance(text);
            }, this);
        }

        public void PlayMessage(string message, Vector2 position, FontSettings settings, UIAnimation showAnimation, UIAnimation hideAnimation, float showAnimationTime, float hideAnimationTime, float limitLife = 1.0f)
        {
            AbstractUGUIText text = textPool.GetInstance();
            text.RectTransform.anchoredPosition = position;

            InitializeUGUIText(text, message, settings);

            StartCoroutine(showAnimation.GetAnimation(text, showAnimationTime).OnCompleted(() =>
            {
                if (limitLife <= 0.0f)
                {
                    StartCoroutine(hideAnimation.GetAnimation(text, hideAnimationTime).OnCompleted(() =>
                    {
                        textPool.ReturnInstance(text);
                    }));
                }
                else
                {
                    KKUtilities.Delay(limitLife, () =>
                    {
                        StartCoroutine(hideAnimation.GetAnimation(text, hideAnimationTime).OnCompleted(() =>
                        {
                            textPool.ReturnInstance(text);
                        }));
                    }, this);
                }
            }));
        }
        public void PlayMessage(string message, Vector3 position, FontSettings settings, UIAnimation showAnimation, UIAnimation hideAnimation, float showAnimationTime, float hideAnimationTime, float limitLife = 1.0f)
        {
            AbstractUGUIText text = textPool.GetInstance();
            text.transform.position = uiCamera.WorldToScreenPoint(position);

            InitializeUGUIText(text, message, settings);

            StartCoroutine(showAnimation.GetAnimation(text, showAnimationTime).OnCompleted(() =>
            {
                if (limitLife <= 0.0f)
                {
                    StartCoroutine(hideAnimation.GetAnimation(text, hideAnimationTime).OnCompleted(() =>
                    {
                        textPool.ReturnInstance(text);
                    }));
                }
                else
                {
                    KKUtilities.Delay(limitLife, () =>
                    {
                        StartCoroutine(hideAnimation.GetAnimation(text, hideAnimationTime).OnCompleted(() =>
                        {
                            textPool.ReturnInstance(text);
                        }));
                    }, this);
                }
            }));
        }

        public void ShowMessage3D(string message, Vector3 position, FontSettings settings, float limitLife = 2.0f)
        {
            AbstractTextMesh text = textMeshPool.GetInstance();
            text.transform.position = position;
            InitializeTextMesh(text, message, settings);

            KKUtilities.Delay(limitLife, () =>
            {
                textMeshPool.ReturnInstance(text);
            }, this);
        }
        public void PlayMessage3D(string message, Vector3 position, FontSettings settings, UIAnimation showAnimation, UIAnimation hideAnimation, float showAnimationTime, float hideAnimationTime, float limitLife = 1.0f)
        {
            AbstractTextMesh text = textMeshPool.GetInstance();
            text.transform.position = position;
            InitializeTextMesh(text, message, settings);

            StartCoroutine(showAnimation.GetAnimation(text, showAnimationTime).OnCompleted(() =>
            {
                if (limitLife <= 0.0f)
                {
                    StartCoroutine(hideAnimation.GetAnimation(text, hideAnimationTime).OnCompleted(() =>
                    {
                        textMeshPool.ReturnInstance(text);
                    }));
                }
                else
                {
                    KKUtilities.Delay(limitLife, () =>
                    {
                        StartCoroutine(hideAnimation.GetAnimation(text, hideAnimationTime).OnCompleted(() =>
                        {
                            textMeshPool.ReturnInstance(text);
                        }));
                    }, this);
                }
            }));
        }
        
        void InitializeUIText(IUIText text, string message, FontSettings settings)
        {
            text.Text = message;
            text.Color = settings.color;
            text.FontSize = settings.fontSize;
            text.Alignment = settings.anchor;
        }
        void InitializeUGUIText(AbstractUGUIText text, string message, FontSettings settings)
        {
            InitializeUIText(text, message, settings);
            text.transform.localScale = Vector3.one;
            text.gameObject.SetActive(true);
        }
        void InitializeTextMesh(AbstractTextMesh text, string message, FontSettings settings)
        {
            InitializeUIText(text, message, settings);
            text.transform.localScale = Vector3.one;
            text.gameObject.SetActive(true);
        }
    }
}
