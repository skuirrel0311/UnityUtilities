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
        public static IEnumerator Delay(float duration, Action action)
        {
            yield return new WaitForSeconds(duration);
            action.Invoke();
        }
        /// <summary>
        /// duration秒後にactionを実行する
        /// </summary>
        public static void Delay(float duration, Action action, MonoBehaviour mono)
        {
            mono.StartCoroutine(Delay(duration, action));
        }
        /// <summary>
        /// duration秒後にactionを実行する
        /// </summary>
        public static IEnumerator Delay(int frameCount, Action action)
        {
            for(int i = 0;i< frameCount;i++)
            {
                yield return null;
            }

            action.Invoke();
        }
        /// <summary>
        /// duration秒後にactionを実行する
        /// </summary>
        public static void Delay(int frameCount, Action action, MonoBehaviour mono)
        {
            mono.StartCoroutine(Delay(frameCount, action));
        }
        
        /// <summary>
        /// 与えられたActionにduration秒かけて０→１になる値を毎フレーム渡す
        /// </summary>
        public static MyCoroutine FloatLerp(float duration, Action<float> action)
        {
            return new MyCoroutine(M_FloatLerp(duration, action));
        }
        static IEnumerator M_FloatLerp(float duration, Action<float> action)
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
        
        /// <summary>
        /// １フレームに１回actionを実行する(updateの戻り値は継続するか？)
        /// </summary>
        public static IEnumerator While(Func<bool> update)
        {
            while(update.Invoke())
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
            while (!predicate()) yield return null;
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
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static IEnumerator WaitAction(MyUnityEvent action)
        {
            bool isCalled = false;
            UnityAction act = () => isCalled = true;

            action.AddListener(act);

            yield return new WaitUntil(() => isCalled);

            action.RemoveListener(act);
        }

        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static IEnumerator WaitAction<T>(MyUnityEvent<T> action)
        {
            bool isCalled = false;
            UnityAction<T> act = ((arg) => isCalled = true);

            action.AddListener(act);

            yield return new WaitUntil(() => isCalled);

            action.RemoveListener(act);
        }

        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static IEnumerator WaitAction<T1, T2>(MyUnityEvent<T1, T2> action)
        {
            bool isCalled = false;
            UnityAction<T1, T2> act = ((arg1, arg2) => isCalled = true);

            action.AddListener(act);

            yield return new WaitUntil(() => isCalled);

            action.RemoveListener(act);
        }

        /// <summary>
        /// 指定された確率でtrueを返す
        /// </summary>
        public static bool GetRandomBool(int probability)
        {
            return UnityEngine.Random.Range(0, 100) < probability;
        }
    }

    public class MyUnityEvent : UnityEvent { }
    public class MyUnityEvent<T> : UnityEvent<T> { }
    public class MyUnityEvent<T1, T2> : UnityEvent<T1, T2> { }
}