using UnityEngine;
namespace KanekoUtilities
{
    public abstract class UGUIParts : PoolMonoBehaviour
    {
        RectTransform rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                {
                    rectTransform = transform as RectTransform;
                }
                return rectTransform;
            }
        }
        public abstract Color Color { get; set; }
        public virtual float Alpha
        {
            get
            {
                return Color.a;
            }

            set
            {
                if (Color.a == value) return;
                Color col = Color;
                col.a = value;
                Color = col;
            }
        }
    }
}