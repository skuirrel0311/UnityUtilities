using System.Collections;
using System;
using UnityEngine;

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
    }

    public class MyCoroutine : IEnumerator
    {
        IEnumerator logic;
        Action onCompleted;
        Action onStart;
        Action onUpdate;

        public MyCoroutine(IEnumerator logic)
        {
            this.logic = logic;
        }

        public bool MoveNext()
        {
            return Start().MoveNext();
        }
        public void Reset()
        {
            Start().Reset();
        }
        public object Current
        {
            get
            {
                return Start().Current;
            }
        }

        IEnumerator Start()
        {
            if (onStart != null) onStart.Invoke();
            while (logic.MoveNext())
            {
                if (onUpdate != null) onUpdate.Invoke();
                yield return null;
            }

            if (onCompleted != null)
            {
                onCompleted.Invoke();
                onCompleted = null;
            }
        }

        public MyCoroutine OnCompleted(Action onCompleted)
        {
            this.onCompleted += onCompleted;
            return this;
        }
        public MyCoroutine OnStart(Action onStart)
        {
            this.onStart += onStart;
            return this;
        }
        public MyCoroutine OnUpdate(Action onUpdate)
        {
            this.onUpdate += onUpdate;
            return this;
        }

        public void CallCompletedSelf()
        {
            if (onCompleted != null) onCompleted.Invoke();
        }
    }

    public class Singleton<T> where T : class, new()
    {
        private static readonly T instance = new T();

        public static T I
        {
            get
            {
                return instance;
            }
        }
    }
}