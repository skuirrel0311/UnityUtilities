using System;
using System.Linq;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HyperCasual
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// MemberInfoに指定した型の属性が付与されているかどうかを調べます。
        /// </summary>
        /// <param name="inherit">メンバーの継承元クラスを検索対象に加えるかどうか</param>
        /// <typeparam name="T">検索する属性</typeparam>
        public static bool HasCustomAttribute<T>(this MemberInfo mi, bool inherit) where T : Attribute {
            return mi.GetCustomAttributes(typeof(T), inherit).Any();
        }
    }
}
