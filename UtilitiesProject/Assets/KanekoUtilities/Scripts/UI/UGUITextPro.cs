using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KanekoUtilities
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UGUITextPro : UGUIText
    {
        TextMeshProUGUI message;
        public TextMeshProUGUI Message
        {
            get
            {
                if (message == null)
                {
                    message = GetComponent<TextMeshProUGUI>();
                }

                return message;
            }
        }

        //よく使うやつはプロパティを用意しておく
        public override string Text
        {
            get
            {
                return Message.text;
            }
            set
            {
                if (Message.text == value) return;
                Message.text = value;
            }
        }
        public override int FontSize
        {
            get
            {
                return (int)Message.fontSize;
            }
            set
            {
                if (FontSize == value) return;
                Message.fontSize = value;
            }
        }
        public override TextAnchor Alignment
        {
            get
            {
                return TextOptionToAnchor(Message.alignment);
            }
            set
            {
                if (Alignment == value) return;
                Message.alignment = TextAnchorToOption(value);
            }
        }
        public override Color Color
        {
            get
            {
                return Message.color;
            }
            set
            {
                if (Color == value) return;
                Message.color = value;
            }
        }
        public override float Alpha
        {
            get
            {
                return Color.a;
            }

            set
            {
                Color col = Color;
                col.a = value;
                Color = col;
            }
        }

        TextAnchor TextOptionToAnchor(TextAlignmentOptions options)
        {
            switch (options)
            {
                case TextAlignmentOptions.TopLeft: return TextAnchor.UpperLeft;
                case TextAlignmentOptions.Top: return TextAnchor.UpperCenter;
                case TextAlignmentOptions.TopRight: return TextAnchor.UpperRight;
                case TextAlignmentOptions.MidlineLeft: return TextAnchor.MiddleLeft;
                case TextAlignmentOptions.Midline: return TextAnchor.MiddleCenter;
                case TextAlignmentOptions.MidlineRight: return TextAnchor.MiddleRight;
                case TextAlignmentOptions.BaselineLeft: return TextAnchor.LowerLeft;
                case TextAlignmentOptions.Baseline: return TextAnchor.LowerCenter;
                case TextAlignmentOptions.BaselineRight: return TextAnchor.LowerRight;
            }

            return 0;
        }
        TextAlignmentOptions TextAnchorToOption(TextAnchor anchor)
        {
            switch (anchor)
            {
                case TextAnchor.UpperLeft: return TextAlignmentOptions.TopLeft;
                case TextAnchor.UpperCenter: return TextAlignmentOptions.Top;
                case TextAnchor.UpperRight: return TextAlignmentOptions.TopRight;
                case TextAnchor.MiddleLeft: return TextAlignmentOptions.MidlineLeft;
                case TextAnchor.MiddleCenter: return TextAlignmentOptions.Midline;
                case TextAnchor.MiddleRight: return TextAlignmentOptions.MidlineRight;
                case TextAnchor.LowerLeft: return TextAlignmentOptions.BaselineLeft;
                case TextAnchor.LowerCenter: return TextAlignmentOptions.Baseline;
                case TextAnchor.LowerRight: return TextAlignmentOptions.BaselineRight;
            }

            return 0;
        }
    }
}