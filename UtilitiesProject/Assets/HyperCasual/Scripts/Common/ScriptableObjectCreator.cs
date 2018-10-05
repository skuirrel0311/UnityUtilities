using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual
{
    public static class ScriptableObjectCreator
    {
        /// <summary>
        /// 指定したパスに、class T の ScriptableObject のアセットを作成・保存します。
        /// EDITOR 以外では、インスタンスのみ作成します。
        /// </summary>
        /// <returns>The asset.</returns>
        /// <param name="directoryPath">Directory path.</param>
        /// <typeparam name="T">ScriptableObject のクラス。</typeparam>
        public static T CreateAsset<T> (string directoryPath) where T : ScriptableObject
        {
            var instance = CreateInstance<T> ();
            #if UNITY_EDITOR
            IOUtility.AssetDatabase.SaveScriptableObject (directoryPath, instance);
            #endif
            Debug.LogFormat ("Creating {0} asset at {1}.", instance.name, directoryPath);
            return instance;
        }

        /// <summary>
        /// class T の ScriptableObject のインスタンスを、名前=クラス名で作成します。
        /// </summary>
        /// <returns>The instance.</returns>
        /// <typeparam name="T">ScriptableObject のクラス。</typeparam>
        public static T CreateInstance<T> () where T : ScriptableObject
        {
            T obj = ScriptableObject.CreateInstance <T> ();
            obj.name = typeof(T).Name;

            return obj;
        }
    }
}