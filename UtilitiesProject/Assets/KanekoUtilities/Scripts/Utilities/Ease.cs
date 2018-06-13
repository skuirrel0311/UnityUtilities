using UnityEngine;

namespace KanekoUtilities
{
    public enum Ease
    {
        Linear,
        InQuad, InCubic, InQuart, InQuint, InExpo, InBack,
        OutQuad, OutCubic, OutQuart, OutQuint, OutExpo, OutBack,
        InOutQuad, InOutCubic, InOutQuart, InOutQuint, InOutExpo, InOutBack
    }
    public static class Easing
    {
        public static float Linear(float x)
        {
            return x;
        }

        public static float InQuad(float x)
        {
            return x * x;
        }
        public static float OutQuad(float x)
        {
            return 1.0f - InQuad(1 - x);
        }
        public static float InOutQuad(float x)
        {
            return x < 0.5f ?
                2.0f * InQuad(x) :
                1.0f - InQuad(-2.0f * x + 2.0f) * 0.5f;
        }

        public static float InCubic(float x)
        {
            return x * x * x;
        }
        public static float OutCubic(float x)
        {
            return 1.0f - InCubic(1.0f - x);
        }
        public static float InOutCubic(float x)
        {
            return x < 0.5f ?
                4.0f * x * x * x :
                1.0f - InCubic(-2.0f * x + 2.0f) * 0.5f;
        }

        public static float InQuart(float x)
        {
            return x * x * x * x;
        }
        public static float OutQuart(float x)
        {
            return 1.0f - InQuart(1.0f - x);
        }
        public static float InOutQuart(float x)
        {
            return x < 0.5f ?
                8.0f * x * x * x * x :
                1.0f - InQuart(-2.0f * x + 2.0f) * 0.5f;
        }

        public static float InQuint(float x)
        {
            return x * x * x * x * x;
        }
        public static float OutQuint(float x)
        {
            return 1.0f - InQuint(1.0f - x);
        }
        public static float InOutQuint(float x)
        {
            return x < 0.5f ?
                16.0f * x * x * x * x * x :
                1.0f - InQuint(-2.0f * x + 2.0f) * 0.5f;
        }

        public static float InExpo(float x)
        {
            return x == 0 ? 0 : Mathf.Pow(2.0f, 10.0f * x - 10.0f);
        }
        public static float OutExpo(float x)
        {
            return x == 1 ? 1 : 1 - Mathf.Pow(2.0f, -10.0f * x);
        }
        public static float InOutExpo(float x)
        {
            if (x == 0.0f) return 0.0f;
            if (x == 1.0f) return 1.0f;

            return x < 0.5f ?
                Mathf.Pow(2.0f, 20.0f * x - 10.0f) * 0.5f :
                (2.0f - Mathf.Pow(2.0f, -20.0f * x + 10.0f)) * 0.5f;
        }

        public static float InBack(float t, float s = 1.70158f)
        {
            return t * t * ((s + 1.0f) * t - s);
        }
        public static float OutBack(float t, float s = 1.70158f)
        {
            return 1.0f - InBack(1.0f - t, s);
        }
        public static float InOutBack(float t, float s = 1.70158f)
        {
            return t < 0.5f ? InBack(t * 2.0f, s) * 0.5f :
                OutBack(t * 2.0f - 1.0f, s) * 0.5f + 0.5f;
        }

        public static float GetEase(float t, Ease ease)
        {
            switch (ease)
            {
                case Ease.Linear: return Linear(t);

                case Ease.InQuad: return InQuad(t);
                case Ease.InCubic: return InCubic(t);
                case Ease.InQuart: return InQuart(t);
                case Ease.InQuint: return InQuint(t);
                case Ease.InExpo: return InExpo(t);
                case Ease.InBack: return InBack(t);

                case Ease.OutQuad: return OutQuad(t);
                case Ease.OutCubic: return OutCubic(t);
                case Ease.OutQuart: return OutQuart(t);
                case Ease.OutQuint: return OutQuint(t);
                case Ease.OutExpo: return OutExpo(t);
                case Ease.OutBack: return OutBack(t);

                case Ease.InOutQuad: return InOutQuad(t);
                case Ease.InOutCubic: return InOutCubic(t);
                case Ease.InOutQuart: return InOutQuart(t);
                case Ease.InOutQuint: return InOutQuint(t);
                case Ease.InOutExpo: return InOutExpo(t);
                case Ease.InOutBack: return InOutBack(t);
            }

            return t;
        }
    }
}