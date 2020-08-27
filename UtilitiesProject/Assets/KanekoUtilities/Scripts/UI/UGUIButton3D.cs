using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    /// <summary>
    /// 現状では綺麗にアルファがかからないのでアルファで消すとかはやならいほうがよい
    /// </summary>
    public class UGUIButton3D : UGUIButton
    {
        [SerializeField]
        bool autoShadowColor = false;
        [SerializeField]
        Color buttonColor = Color.white;

        [Space]
        [SerializeField]
        UGUIImage buttonImage = null;

        [SerializeField]
        UGUIImage shadowImage = null;
        
        void OnValidate()
        {
            if (!autoShadowColor) return;
            var c = buttonColor;
            float h, s, v;
            Color.RGBToHSV(c, out h, out s, out v);
            v -= 0.2f;

            buttonImage.Color = buttonColor;
            shadowImage.Color = Color.HSVToRGB(h, s, v);
        }

        protected override void OnClick()
        {
            Button.interactable = false;
            Vector3 startPos = buttonImage.transform.position;
            Vector3 endPos = shadowImage.transform.position;

            StartCoroutine(KKUtilities.FloatLerp(0.15f, (t) =>
            {
                buttonImage.transform.position = Vector3.Lerp(startPos, endPos, Easing.Yoyo(t));
            }, false).OnCompleted(() =>
            {
                base.OnClick();
                Button.interactable = true;
            }));
        }
    }
}
