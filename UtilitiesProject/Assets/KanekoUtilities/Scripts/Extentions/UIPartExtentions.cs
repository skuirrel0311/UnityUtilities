using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public static class UIPartExtentions
    {
        public static Coroutine Fade(this UGUIParts self, float duration, float targetAlpha)
        {
            var startAlpha = self.Alpha;
            return self.StartCoroutine(KKUtilities.FloatLerp(duration, (t) =>
            {
                self.Alpha = Mathf.Lerp(startAlpha, targetAlpha, Easing.InQuad(t));
            }));
        }
        
        public static Coroutine PopUp(this UGUIParts self, float duration)
        {
            self.transform.localScale = Vector3.zero;
            self.gameObject.SetActive(true);
            return self.StartCoroutine(KKUtilities.FloatLerp(duration, (t) =>
            {
                self.transform.localScale = Vector3.LerpUnclamped(Vector3.zero, Vector3.one, Easing.OutBack(t));
            }));
        }
    }
}