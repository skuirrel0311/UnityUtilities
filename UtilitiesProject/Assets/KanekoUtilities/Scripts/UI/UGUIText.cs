using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Text))]
    public class UGUIText : PoolMonoBehaviour
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
        public Color Color
        {
            get
            {
                return Message.color;
            }
            set
            {
                if (Message.color == value) return;
                Message.color = value;
            }
        }
    }
}