using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace KanekoUtilities
{
    public class KKUtilities
    {
        /// <summary>
        /// duration秒後にactionを実行する
        /// </summary>
        public static IEnumerator Delay(float duration, Action action, bool isScalable = true)
        {
            if (isScalable)
                yield return new WaitForSeconds(duration);
            else
                yield return new WaitForSecondsRealtime(duration);

            action.Invoke();
        }
        /// <summary>
        /// duration秒後にactionを実行する
        /// </summary>
        public static void Delay(float duration, Action action, MonoBehaviour mono, bool isScalable = true)
        {
            mono.StartCoroutine(Delay(duration, action));
        }

        /// <summary>
        /// frameCountフレーム後にactionを実行する
        /// </summary>
        public static IEnumerator DelayFrame(int frameCount, Action action)
        {
            for (int i = 0; i < frameCount; i++)
            {
                yield return null;
            }

            action.Invoke();
        }
        /// <summary>
        /// frameCountフレーム後にactionを実行する
        /// </summary>
        public static void DelayFrame(int frameCount, Action action, MonoBehaviour mono)
        {
            mono.StartCoroutine(DelayFrame(frameCount, action));
        }

        /// <summary>
        /// 与えられたActionにduration秒かけて０→１になる値を毎フレーム渡す
        /// </summary>
        public static MyCoroutine FloatLerp(float duration, Action<float> action, bool isScalable = true)
        {
            if (isScalable)
                return new MyCoroutine(ScalableFloatLerp(duration, action));
            else
                return new MyCoroutine(UnscalableFloatLerp(duration, action));
        }
        /// <summary>
        /// 与えられたActionにduration秒かけて０→１になる値を毎フレーム渡す
        /// </summary>
        public static void FloatLerp(float duration, Action<float> action, MonoBehaviour mono, bool isScalable = true)
        {
            if (isScalable)
                mono.StartCoroutine(ScalableFloatLerp(duration, action));
            else
                mono.StartCoroutine(UnscalableFloatLerp(duration, action));
        }
        static IEnumerator ScalableFloatLerp(float duration, Action<float> action)
        {
            float t = 0.0f;

            while (true)
            {
                t += Time.deltaTime;
                action.Invoke(t / duration);
                if (t > duration) break;
                yield return null;
            }

            action.Invoke(1.0f);
        }
        static IEnumerator UnscalableFloatLerp(float duration, Action<float> action)
        {
            float t = 0.0f;

            while (true)
            {
                t += Time.unscaledDeltaTime;
                action.Invoke(t / duration);
                if (t > duration) break;
                yield return null;
            }

            action.Invoke(1.0f);
        }

        /// <summary>
        /// １フレームに１回actionを実行する(updateの戻り値は継続するか？)
        /// </summary>
        public static IEnumerator While(Func<bool> update)
        {
            while (update.Invoke())
            {
                yield return null;
            }
        }
        /// <summary>
        /// １フレームに１回actionを実行する(updateの戻り値は継続するか？)
        /// </summary>
        public static void While(Func<bool> update, MonoBehaviour mono)
        {
            mono.StartCoroutine(While(update));
        }

        /// <summary>
        /// predicateが真を返すまで待機し、その後actionを実行する
        /// </summary>
        public static IEnumerator WaitUntil(Func<bool> predicate, Action action)
        {
            yield return new WaitUntil(predicate);

            action.Invoke();
        }
        /// <summary>
        /// predicateが真を返すまで待機し、その後actionを実行する
        /// </summary>
        public static void WaitUntil(Func<bool> predicate, Action action, MonoBehaviour mono)
        {
            mono.StartCoroutine(WaitUntil(predicate, action));
        }

        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static IEnumerator WaitAction(UnityEvent action, Action callback = null)
        {
            bool isCalled = false;
            UnityAction act = () => isCalled = true;

            action.AddListener(act);

            yield return new WaitUntil(() => isCalled);

            action.RemoveListener(act);
            if (callback != null) callback.Invoke();
        }
        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static IEnumerator WaitAction<T>(MyUnityEvent<T> action, Action callback = null)
        {
            bool isCalled = false;
            UnityAction<T> act = ((arg) => isCalled = true);

            action.AddListener(act);

            yield return new WaitUntil(() => isCalled);

            action.RemoveListener(act);
            if (callback != null) callback.Invoke();
        }
        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static IEnumerator WaitAction<T1, T2>(MyUnityEvent<T1, T2> action, Action callback = null)
        {
            bool isCalled = false;
            UnityAction<T1, T2> act = ((arg1, arg2) => isCalled = true);

            action.AddListener(act);

            yield return new WaitUntil(() => isCalled);

            action.RemoveListener(act);
            if (callback != null) callback.Invoke();
        }
        
        /// <summary>
        /// 0~360の値で返す(-10なら350)
        /// </summary>
        public static float ClampRotation(float rotationValue)
        {
            if (rotationValue == 0.0f) return 0.0f;
            float clampValue = 0.0f;

            int temp = (int)(Mathf.Abs(rotationValue) / 360.0f);
            if (rotationValue > 0)
            {
                clampValue = rotationValue - (360.0f * temp);
            }
            else
            {
                clampValue = rotationValue + (360.0f * (temp + 1));
            }

            return clampValue;
        }
        
        static readonly string[] UnitTexts = { "k", "m", "b", "t", "q" };

        /// <summary>
        /// 桁の多い数字をstringに変換する
        /// 
        /// ex.
        /// 1000->1.00k
        /// 10000->10.0k
        /// </summary>
        public static string GetDigitText(long number)
        {
            int digit = GetDigitNum(number);

            if (digit < 4) return number.ToString();

            int temp = ((digit - 1) / 3);
            int unitTextIndex = temp - 1;

            if (unitTextIndex >= UnitTexts.Length) return "infinite";
            string unitText = UnitTexts[unitTextIndex];

            long unitValue = LongPow(10, (temp * 3) - 1);

            string result;
            temp = (digit - 1) % 3;

            float resultValue = (float)number / unitValue;

            if (temp == 0)
                result = resultValue.ToString("0.00");
            else if (temp == 1)
                result = resultValue.ToString("00.0");
            else
                result = resultValue.ToString("000");

            return result + unitText;
        }

        /// <summary>
        /// 桁数を返す
        /// </summary>
        public static int GetDigitNum(long number)
        {
            int digitNum = 1;

            while (true)
            {
                number = (int)(number * 0.1f);

                if (number < 1) break;

                digitNum++;
            }

            return digitNum;
        }

        public static long LongPow(long x, long y)
        {
            if (y == 0) return 1;
            if (y == 1) return x;

            long value = x;

            for (int i = 0; i < y; i++)
            {
                value *= x;
            }

            return value;
        }
    }

    public class MyUnityEvent : UnityEvent { }
    public class MyUnityEvent<T> : UnityEvent<T> { }
    public class MyUnityEvent<T1, T2> : UnityEvent<T1, T2> { }
}