using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public static class RandomUtil
    {
        /// <summary>
        /// 指定された確率でtrueを返す
        /// </summary>
        public static bool GetRandomBool(int probability)
        {
            return Random.Range(0, 100) < probability;
        }

        /// <summary>
        /// 渡された配列の中からランダムで1つを返す
        /// </summary>
        public static T GetRandomValue<T>(T[] values)
        {
            return values[Random.Range(0, values.Length)];
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

            int randomValue = Random.Range(1, totalWeight + 1);
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
}
