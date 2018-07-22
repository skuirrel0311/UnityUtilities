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
        public static void FlaotLerp(float duration, Action<float> action, MonoBehaviour mono, bool isScalable = true)
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
        public static void WiatUntil(Func<bool> predicate, Action action, MonoBehaviour mono)
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
        /// ２点間の(Y成分に限定した)角度を返す
        /// </summary>
        public static float GetAngleY(Vector3 vec1, Vector3 vec2)
        {
            Vector3 temp = vec2 - vec1;
            float vecY = temp.y;
            //X方向だけのベクトルに変換
            temp = Vector3.right * temp.magnitude;
            temp.y = vecY;

            return Vector3.Angle(Vector3.right, temp) * 2.0f;
        }

        /// <summary>
        /// 指定した角度の球体座標を返す
        /// </summary>
        /// <param name="longitude">経度</param>
        /// <param name="latitude">緯度</param>
        public static Vector3 SphereCoordinate(float longitude, float latitude, float distance)
        {
            Vector3 position = Vector3.zero;

            //重複した計算
            float temp1 = distance * Mathf.Cos(latitude * Mathf.Deg2Rad);
            float temp2 = longitude * Mathf.Deg2Rad;

            position.x = temp1 * Mathf.Sin(temp2);
            position.y = distance * Mathf.Sin(latitude * Mathf.Deg2Rad);
            position.z = temp1 * Mathf.Cos(temp2);

            return position;
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
        
        /// <summary>
        /// 指定された確率でtrueを返す
        /// </summary>
        public static bool GetRandomBool(int probability)
        {
            return UnityEngine.Random.Range(0, 100) < probability;
        }

        /// <summary>
        /// 渡された配列の中からランダムで1つを返す
        /// </summary>
        public static T GetRandomValue<T>(T[] values)
        {
            return values[UnityEngine.Random.Range(0, values.Length)];
        }

        /// <summary>
        /// 配列の要素の重みを考慮して要素のインデックスを返す
        /// </summary>
        public static int GetRandomIndexWithWeight(params int[] weightArray)
        {
            int totalWeight = 0;
            for (int i = 0; i < weightArray.Length; i++)
            {
                totalWeight += weightArray[i];
            }

            int randomValue = UnityEngine.Random.Range(1, totalWeight + 1);
            int index = -1;
            for (var i = 0; i < weightArray.Length; ++i)
            {
                if (weightArray[i] >= randomValue)
                {
                    index = i;
                    break;
                }
                randomValue -= weightArray[i];
            }
            return index;
        }
    }

    public class MyUnityEvent : UnityEvent { }
    public class MyUnityEvent<T> : UnityEvent<T> { }
    public class MyUnityEvent<T1, T2> : UnityEvent<T1, T2> { }
}