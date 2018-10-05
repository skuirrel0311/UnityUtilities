using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HyperCasual
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 配列をランダムに並べ替えた配列を取得します。副作用はありません。
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> em)
        {
            return em.OrderBy(_ => Random.value);
        }

        /// <summary>
        /// ランダムに要素を1つ取得します。
        /// </summary>
        public static T Sample<T>(this IEnumerable<T> em)
        {
            return em.ElementAt(Random.Range(0, em.Count()));
        }
        /// <summary>
        /// ランダムに要素を1つ取得します。
        /// </summary>
        public static T SampleOrDefault<T>(this IEnumerable<T> em)
        {
            return em.ElementAtOrDefault(Random.Range(0, em.Count()));
        }


        #region Number

        /// <summary>
        /// 数値の配列を重みにして、ランダムに index を取得します。
        /// </summary>
        public static int GetRandomIndex(this IEnumerable<float> rates)
        {
            #if UNITY_EDITOR
            IsValidRates(rates);
            #endif

            float sum = rates.Sum(rate => Mathf.Max(0, rate));
            int count = rates.Count();
            if (sum <= 0 || count == 0) {
                return -1;
            }

            float target = Random.value * sum;
            float pointer = 0f;
            for (int i = 0; i < count; i++) {
                float diff = Mathf.Max(0, rates.ElementAt(i));
                if (diff <= 0) {
                    continue;
                }

                pointer += diff;
                if (target <= pointer) {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 数値の配列を重みにして、ランダムに index を取得します。
        /// </summary>
        public static int GetRandomIndex(this IEnumerable<int> rates)
        {
            return rates.Select(item => (float)item).GetRandomIndex();
        }
        private static bool IsValidRates(IEnumerable<float> rates)
        {
            if (rates.Any(rate => rate < 0)) {
                Debug.LogError("配列の中に負の値が存在します。");
                return false;
            }
            return true;
        }


        /// <summary>
        /// 数値の配列を元に、float 値を N~M の形のグループ名に変換します。
        /// 数値の配列は、すべてユニークで正の値、ソート済みである必要があります。
        /// </summary>
        /// <returns>The group name.</returns>
        /// <param name="groups">グループ毎の最大値の配列。</param>
        /// <param name="param">対象のパラメータ値。</param>
        public static string GetGroupName(this IEnumerable<float> groups, float param)
        {
            #if UNITY_EDITOR
            IsValidGroups(groups);
            #endif
            var lastValue = 0.0f;
            foreach (var v in groups) {
                if (param <= v) {
                    return string.Format("{0}~{1}", lastValue, v);
                } else {
                    lastValue = v;
                }
            }
            return string.Format("{0}~", lastValue);
        }
        /// <summary>
        /// 数値の配列を元に、int 値を N~M の形のグループ名に変換します。
        /// 数値の配列は、すべてユニークで正の値、ソート済みである必要があります。
        /// </summary>
        /// <returns>The group name.</returns>
        /// <param name="groups">グループ毎の最大値の配列。</param>
        /// <param name="param">対象のパラメータ値。</param>
        public static string GetGroupName(this IEnumerable<int> groups, int param)
        {
            #if UNITY_EDITOR
            IsValidGroups(groups.Select(item => (float)item));
            #endif
            var lastValue = 0;
            foreach (var v in groups) {
                if (param <= v) {
                    return string.Format("{0}~{1}", lastValue, v);
                } else {
                    lastValue = v + 1;
                }
            }
            return string.Format("{0}~", lastValue);
        }
        private static bool IsValidGroups(IEnumerable<float> groups)
        {
            var count = groups.Count();
            if (count <= 0) {
                return true;
            }

            if (groups.Any(group => group < 0)) {
                Debug.LogError("配列の中に負の値が存在します。");
                return false;
            }

            if (groups.Distinct().Count() < count) {
                Debug.LogError("配列の中に同じ値が複数存在します。");
                return false;
            }

            for (var i = 0; i < count - 1; i++) {
                var current = groups.ElementAt(i);
                var next = groups.ElementAt(i + 1);
                if (current >= next) {
                    Debug.LogError("配列が昇順にソートされていません。");
                    return false;
                }
            }

            return true;
        }

        #endregion


        #region Object

        /// <summary>
        /// UnityEngine.Object が null になっているものをのぞいた配列を取得します。
        /// </summary>
        public static IEnumerable<T> AliveObjects<T>(this IEnumerable<T> objects) where T : UnityEngine.Object
        {
            return objects.Where(obj => (UnityEngine.Object)obj != (UnityEngine.Object)null);
        }
        /// <summary>
        /// UnityEngine.Component が null になっているものと、アタッチされた GameObject が null になっているものをのぞいた配列を取得します。
        /// </summary>
        public static IEnumerable<T> Alives<T>(this IEnumerable<T> components) where T : UnityEngine.Component
        {
            return components.Where(comp => (UnityEngine.Object)comp != (UnityEngine.Object)null && comp.gameObject != null);
        }

        #endregion
    }
}