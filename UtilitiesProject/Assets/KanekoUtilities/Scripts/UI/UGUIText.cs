using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Text))]
    public class UGUIText : UGUIParts
    {
        Text message;
        public Text Message
        {
            get
            {
                if (message == null)
                {
                    message = GetComponent<Text>();
                }

                return message;
            }
        }

        //よく使うやつはプロパティを用意しておく
        public string Text
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
        public int FontSize
        {
            get
            {
                return Message.fontSize;
            }
            set
            {
                if (FontSize == value) return;
                Message.fontSize = value;
            }
        }
        public TextAnchor Alignment
        {
            get
            {
                return Message.alignment;
            }
            set
            {
                if (Alignment == value) return;
                Message.alignment = value;
            }
        }
    }
}