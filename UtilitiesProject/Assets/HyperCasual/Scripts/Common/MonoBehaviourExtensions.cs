using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual
{
    public static class MonoBehaviourExtensions
    {
        #region Next

        /// <summary>
        /// 次のフレームで実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="action">Action.</param>
        public static Coroutine DoOnNextFrame(this MonoBehaviour self, Action action)
        {
            if (action == null) {
                // do nothing
                return null;
            } else {
                return self.StartCoroutine(DoOnNextFrameCoroutine(action));
            }
        }
        static IEnumerator DoOnNextFrameCoroutine(Action action)
        {
            yield return null;
            action();
            yield break;
        }

        /// <summary>
        /// EndOfFrame のタイミングで実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="action">Action.</param>
        public static Coroutine DoOnEndOfFrame(this MonoBehaviour self, Action action)
        {
            if (action == null) {
                // do nothing
                return null;
            } else {
                return self.StartCoroutine(DoOnEndOfFrameCoroutine(action));
            }
        }
        static IEnumerator DoOnEndOfFrameCoroutine(Action action)
        {
            yield return new WaitForEndOfFrame();
            action();
            yield break;
        }

        /// <summary>
        /// FixedUpdate のタイミングで実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="action">Action.</param>
        public static Coroutine DoOnNextFixedUpdate(this MonoBehaviour self, Action action)
        {
            if (action == null) {
                // do nothing
                return null;
            } else {
                return self.StartCoroutine(DoOnFixedUpdateCoroutine(action));
            }
        }
        static IEnumerator DoOnFixedUpdateCoroutine(Action action)
        {
            yield return new WaitForFixedUpdate();
            action();
            yield break;
        }

        #endregion



        #region After

        /// <summary>
        /// 指定したフレーム後に実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="frames">Frames.</param>
        /// <param name="action">Action.</param>
        public static Coroutine DoAfterFrames(this MonoBehaviour self, int frames, Action action)
        {
            if (action == null) {
                // do nothing
                return null;
            } else if (frames <= 0) {
                action();
                return null;
            } else {
                return self.StartCoroutine(DoAfterFramesCoroutine(frames, action));
            }
        }
        static IEnumerator DoAfterFramesCoroutine(int frames, Action action)
        {
            for (var i = 0; i < frames; i++) {
                yield return null;
            }
            action();
            yield break;
        }

        /// <summary>
        /// 指定した秒数後に実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="seconds">秒数。</param>
        /// <param name="action">Action.</param>
        public static Coroutine DoAfterSeconds(this MonoBehaviour self, float seconds, Action action)
        {
            if (action == null) {
                // do nothing
                return null;
            } else if (seconds <= 0.0f) {
                action();
                return null;
            } else {
                return self.StartCoroutine(DoAfterSecondsCoroutine(seconds, action));
            }
        }
        static IEnumerator DoAfterSecondsCoroutine(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action();
            yield break;
        }

        /// <summary>
        /// 指定した秒数後の EndOfFrame のタイミングで実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="seconds">秒数。</param>
        /// <param name="action">Action.</param>
        public static Coroutine DoAfterSecondsOnEndOfFrame(this MonoBehaviour self, float seconds, Action action)
        {
            if (action == null) {
                // do nothing
                return null;
            } else if (seconds <= 0.0f) {
                action();
                return null;
            } else {
                return self.StartCoroutine(DoAfterSecondsOnEndOfFrameCoroutine(seconds, action));
            }
        }
        static IEnumerator DoAfterSecondsOnEndOfFrameCoroutine(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            yield return new WaitForEndOfFrame();
            action();
            yield break;
        }


        /// <summary>
        /// 指定した秒数後の FixedUpdate で実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="seconds">秒数。</param>
        /// <param name="action">Action.</param>
        public static Coroutine DoAfterSecondsOnFixedUpdate(this MonoBehaviour self, float seconds, Action action)
        {
            if (action == null) {
                // do nothing
                return null;
            } else if (seconds <= 0.0f) {
                action();
                return null;
            } else {
                return self.StartCoroutine(DoAfterSecondsOnFixedUpdateCoroutine(seconds, action));
            }
        }
        static IEnumerator DoAfterSecondsOnFixedUpdateCoroutine(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            yield return new WaitForFixedUpdate();
            action();
            yield break;
        }

        /// <summary>
        /// 指定した(Realtime の)秒数後に実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="seconds">秒数。</param>
        /// <param name="action">Action.</param>
        public static Coroutine DoAfterSecondsRealtime(this MonoBehaviour self, float seconds, Action action)
        {
            if (action == null) {
                // do nothing
                return null;
            } else if (seconds <= 0.0f) {
                action();
                return null;
            } else {
                return self.StartCoroutine(DoAfterSecondsRealtimeCoroutine(seconds, action));
            }
        }
        static IEnumerator DoAfterSecondsRealtimeCoroutine(float seconds, Action action)
        {
            yield return new WaitForSecondsRealtime(seconds);
            action();
            yield break;
        }

        #endregion



        #region Repeat

        /// <summary>
        /// 指定した秒数の間隔で、実行を繰り返します。
        /// 1度目はすぐに実行されます。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="interval">Interval.</param>
        /// <param name="action">Action.引数に繰り返し回数を持ちます。</param>
        public static Coroutine DoRepeatedly(this MonoBehaviour self, float interval, Action<int> action)
        {
            return self.DoRepeatedly(interval, true, action);
        }
        /// <summary>
        /// 指定した秒数の間隔で、実行を繰り返します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="interval">Interval.</param>
        /// <param name="immediate"><c>true</c>ならば、1度目をすぐに実行します。</param>
        /// <param name="action">Action.引数に繰り返し回数を持ちます。</param>
        public static Coroutine DoRepeatedly(this MonoBehaviour self, float interval, bool immediate, Action<int> action)
        {
            return self.DoRepeatedly(interval, immediate, -1, action);
        }
        /// <summary>
        /// 指定した秒数の間隔・回数で、実行を繰り返します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="interval">Interval.</param>
        /// <param name="immediate"><c>true</c>ならば、1度目をすぐに実行します。</param>
        /// <param name="times">繰り返し回数。</param>
        /// <param name="action">Action.引数に繰り返し回数を持ちます。</param>
        public static Coroutine DoRepeatedly(this MonoBehaviour self, float interval, bool immediate, int times, Action<int> action)
        {
            if (action == null || interval <= 0 || times == 0) {
                // do nothing
                return null;
            } else {
                return self.StartCoroutine(DoRepeatedlyCoroutine(interval, immediate, times, action));
            }
        }
        static IEnumerator DoRepeatedlyCoroutine(float interval, bool immediate, int times, Action<int> action)
        {
            int repeated = 0;
            if (immediate) {
                repeated++;
                action(repeated);
            }
            var wait = new WaitForSeconds(interval);
            while (repeated != times) {
                yield return wait;
                repeated++;
                action(repeated);
            }
            yield break;
        }

        #endregion




        #region Every

        /// <summary>
        /// Update のたびに実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="update">Update action.引数に経過秒数を持ちます。</param>
        public static Coroutine DoOnEveryUpdate(this MonoBehaviour self, Action<float> update)
        {
            if (update == null) {
                return null;
            } else {
                return self.StartCoroutine(DoOnEveryUpdateCoroutine(update));
            }
        }
        static IEnumerator DoOnEveryUpdateCoroutine(Action<float> update)
        {
            var time = Time.time;
            while (true) {
                var elapsed = Time.time - time;
                update(elapsed);
                yield return null;
            }
        }

        /// <summary>
        /// 指定した秒数の間、 Update のたびに実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="seconds">Seconds.</param>
        /// <param name="update">Update action.引数に経過秒数を持ちます。</param>
        /// <param name="finish">Finish action.</param>
        public static Coroutine DoOnEveryUpdate(this MonoBehaviour self, float seconds, Action<float> update, Action finish = null)
        {
            if (seconds <= 0) {
                if (finish != null) {
                    finish();
                }
                return null;
            } else if (update == null) {
                return self.DoAfterSeconds(seconds, finish);
            } else {
                return self.StartCoroutine(DoOnEveryUpdateWithSecondsCoroutine(seconds, update, finish));
            }
        }
        static IEnumerator DoOnEveryUpdateWithSecondsCoroutine(float seconds, Action<float> update, Action finish)
        {
            var until = Time.time + seconds;
            while (true) {
                var time = Time.time;
                if (until < time) {
                    break;
                }
                var elapsed = seconds - Mathf.Max(0, until - time);
                update(elapsed);
                yield return null;
            }
            if (finish != null) {
                finish();
            }
            yield break;
        }


        /// <summary>
        /// EndOfFrame のたびに実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="update">Update action.引数に経過秒数を持ちます。</param>
        public static Coroutine DoOnEveryEndOfFrame(this MonoBehaviour self, Action<float> update)
        {
            if (update == null) {
                return null;
            } else {
                return self.StartCoroutine(DoOnEveryEndOfFrameCoroutine(update));
            }
        }
        static IEnumerator DoOnEveryEndOfFrameCoroutine(Action<float> update)
        {
            var time = Time.time;
            var wait = new WaitForEndOfFrame();
            while (true) {
                yield return wait;
                var elapsed = Time.time - time;
                update(elapsed);
            }
        }

        /// <summary>
        /// 指定した秒数の間、 EndOfFrame のたびに実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="seconds">Seconds.</param>
        /// <param name="update">Update action.引数に(繰り返し回数,経過秒数)を持ちます。</param>
        /// <param name="finish">Finish action.引数に繰り返し回数を持ちます。</param>
        public static Coroutine DoOnEveryEndOfFrame(this MonoBehaviour self, float seconds, Action<float> update, Action finish = null)
        {
            if (seconds <= 0) {
                if (finish != null) {
                    finish();
                }
                return null;
            } else if (update == null) {
                return self.DoAfterSecondsOnEndOfFrame(seconds, finish);
            } else {
                return self.StartCoroutine(DoOnEveryEndOfFrameWithSecondsCoroutine(seconds, update, finish));
            }
        }
        static IEnumerator DoOnEveryEndOfFrameWithSecondsCoroutine(float seconds, Action<float> update, Action finish)
        {
            var until = Time.time + seconds;
            var wait = new WaitForEndOfFrame();
            while (true) {
                yield return wait;
                var time = Time.time;
                if (until < time) {
                    break;
                }
                var elapsed = seconds - Mathf.Max(0, until - time);
                update(elapsed);
            }
            if (finish != null) {
                finish();
            }
            yield break;
        }

        /// <summary>
        /// FixedUpdate のたびに実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="update">Update action.引数に経過秒数を持ちます。</param>
        public static Coroutine DoOnEveryFixedUpdate(this MonoBehaviour self, Action<float> update)
        {
            if (update == null) {
                return null;
            } else {
                return self.StartCoroutine(DoOnEveryFixedUpdateCoroutine(update));
            }
        }
        static IEnumerator DoOnEveryFixedUpdateCoroutine(Action<float> update)
        {
            var time = Time.fixedTime;
            var wait = new WaitForFixedUpdate();
            while (true) {
                yield return wait;
                var elapsed = Time.fixedTime - time;
                update(elapsed);
            }
        }

        /// <summary>
        /// 指定した秒数の間、 FixedUpdate のたびに実行します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="seconds">Seconds.</param>
        /// <param name="update">Update action.引数に(繰り返し回数,経過秒数)を持ちます。</param>
        /// <param name="finish">Finish action.引数に繰り返し回数を持ちます。</param>
        public static Coroutine DoOnEveryFixedUpdate(this MonoBehaviour self, float seconds, Action<float> update, Action finish = null)
        {
            if (seconds <= 0) {
                if (finish != null) {
                    finish();
                }
                return null;
            } else if (update == null) {
                return self.DoAfterSecondsOnFixedUpdate(seconds, finish);
            } else {
                return self.StartCoroutine(DoOnEveryFixedUpdateWithSecondsCoroutine(seconds, update, finish));
            }
        }
        static IEnumerator DoOnEveryFixedUpdateWithSecondsCoroutine(float seconds, Action<float> update, Action finish)
        {
            var until = Time.fixedTime + seconds;
            var wait = new WaitForFixedUpdate();
            while (true) {
                yield return wait;
                var time = Time.fixedTime;
                if (until < time) {
                    break;
                }
                var elapsed = seconds - Mathf.Max(0, until - time);
                update(elapsed);
            }
            if (finish != null) {
                finish();
            }
            yield break;
        }


        #endregion



        /// <summary>
        /// 指定した秒数の間、 Update のたびに実行し、繰り返します。
        /// </summary>
        /// <returns>Coroutine.</returns>
        /// <param name="seconds">Seconds.</param>
        /// <param name="update">Update action.引数に(繰り返し回数,経過秒数)を持ちます。</param>
        /// <param name="finish">Finish action.引数に繰り返し回数を持ちます。</param>
        public static Coroutine DoRepeatedlyOnUpdateInSeconds(this MonoBehaviour self, float seconds, Action<int, float> update, Action<int> finish = null)
        {
            if (seconds <= 0) {
                return null;
            } else if (update == null) {
                return self.DoRepeatedly(seconds, false, finish);
            } else {
                return self.StartCoroutine(DoRepeatedlyOnUpdateInSecondsCoroutine(seconds, update, finish));
            }
        }
        static IEnumerator DoRepeatedlyOnUpdateInSecondsCoroutine(float seconds, Action<int, float> update, Action<int> finish)
        {
            int repeated = 0;
            while (true) {
                var until = Time.time + seconds;
                while (true) {
                    var time = Time.time;
                    if (until < time) {
                        break;
                    }
                    var elapsed = seconds - Mathf.Max(0, until - time);
                    update(repeated, elapsed);
                    yield return null;
                }
                repeated++;
                if (finish != null) {
                    finish(repeated);
                }
            }
        }
    }
}