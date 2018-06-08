using UnityEngine;

namespace KanekoUtilities
{
    public static class PositiveWord
    {
        static int currentIndex;
        //scriptableObjectにしてもよいかもしれない
        static string[] words = {
            "gorgeous", "awesome", "fantastic", "marvelous",
            "perfect", "wonderful", "great"
        };

        public static string GetWord()
        {
            if (currentIndex >= words.Length) currentIndex = 0;
            return words[currentIndex++];
        }
    }

    public class PositiveWordDisplayer : SingletonMonobehaviour<PositiveWordDisplayer>
    {
        [SerializeField]
        UGUITextPool textPool = null;

        readonly Vector3 StartTextScale = Vector3.one * 0.3f;
        readonly Vector3 OneVec = Vector3.one;
        
        public void ShowPositiveWord(Vector3 position, float showAnimationTime = 0.3f, float hideAnimationTime = 0.5f)
        {
            ShowPositiveWord(position, Quaternion.identity, showAnimationTime, hideAnimationTime);
        }
        public void ShowPositiveWord(Vector3 position, Vector3 rotationAngle, float showAnimationTime = 0.3f, float hideAnimationTime = 0.5f)
        {
            ShowPositiveWord(position, Quaternion.Euler(rotationAngle), showAnimationTime, hideAnimationTime);
        }
        public void ShowPositiveWord(Vector3 position, Quaternion rotation, float showAnimationTime = 0.3f, float hideAnimationTime = 0.5f)
        {
            UGUIText text = textPool.GetInstance();
            text.Text = PositiveWord.GetWord();
            text.transform.SetPositionAndRotation(position, rotation);

            StartCoroutine(KKUtilities.FloatLerp(showAnimationTime, (t) =>
            {
                text.transform.localScale = Vector3.Lerp(StartTextScale, OneVec, Easing.OutQuad(t));
            }).OnCompleted(() =>
            {
                StartCoroutine(KKUtilities.FloatLerp(hideAnimationTime, (t) =>
                {
                    text.Alpha = Mathf.Lerp(1.0f, 0.0f, t * t);
                }));
            }));
        }
        public void ShowPositiveWord(Vector2 position, Quaternion rotation, float showAnimationTime = 0.3f, float hideAnimationTime = 0.5f)
        {
            UGUIText text = textPool.GetInstance();
            text.Text = PositiveWord.GetWord();
            text.RectTransform.anchoredPosition = position;
            text.RectTransform.rotation = rotation;

            StartCoroutine(KKUtilities.FloatLerp(showAnimationTime, (t) =>
            {
                text.RectTransform.localScale = Vector3.Lerp(StartTextScale, OneVec, Easing.OutQuad(t));
            }).OnCompleted(() =>
            {
                StartCoroutine(KKUtilities.FloatLerp(hideAnimationTime, (t) =>
                {
                    text.Alpha = Mathf.Lerp(1.0f, 0.0f, t * t);
                }));
            }));
        }
    }
}