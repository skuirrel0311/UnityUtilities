using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Image))]
    public class UGUIImage : PoolMonoBehaviour
    {
        Image image;
        public Image Image
        {
            get
            {
                if (image == null)
                {
                    image = GetComponent<Image>();
                }
                return image;
            }
        }

        //よく使うものはプロパティにしておく
        public Sprite Sprite
        {
            get
            {
                return Image.sprite;
            }
            set
            {
                Image.sprite = value;
            }
        }
        public Color Color
        {
            get
            {
                return Image.color;
            }
            set
            {
                Image.color = value;
            }
        }
    }
}
