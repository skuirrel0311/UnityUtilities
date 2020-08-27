using UnityEngine;

namespace KanekoUtilities
{
    public abstract class UGUIParts : UIParts
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
    }
}