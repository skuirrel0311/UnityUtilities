using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace HyperCasual
{
    /// <summary>
    /// 設定ファイルのための Object.
    /// 必ず、Assets/Resources/Settings に配置されて利用されることを想定しています。
    /// </summary>
    public abstract class SettingsObject<T> : ScriptableObject where T : ScriptableObject, IInitializable
    {
        public const string SettingsDirectory = "Settings";

        [NonSerialized] private bool isInitialized = false;
        public bool IsInitialized { 
            get { return isInitialized; }
            set { isInitialized = value; }
        }

        private static T instance;
        public static T Instance {
            get {
                if (instance == null) {
                    instance = Load ();
                    #if UNITY_EDITOR
                    if (Application.isPlaying) {
                        instance.Init ();
                    }
                    #else
                    instance.Init ();
                    #endif
                }
                return instance;
            }
        }

        public static T Load ()
        {
            T settings = LoadFromResources ();
            if (settings == null) {
                settings = Create ();
            }
            return settings;
        }

        public static bool Exists ()
        {
            T settings = LoadFromResources ();
            return (settings != null);
        }

        private static T LoadFromResources ()
        {
            var path = GetResourcesPath ();
            T settings = Resources.Load (path, typeof(T)) as T;
            return settings;
        }

        private static string GetResourcesPath ()
        {
            return System.IO.Path.Combine (SettingsDirectory, typeof(T).Name);
        }

        private static T Create ()
        {
            var settings = ScriptableObjectCreator.CreateInstance<T> ();

            #if UNITY_EDITOR
            var directoryPath = System.IO.Path.Combine (IOUtility.Resources.TopResourcesPath, SettingsDirectory);
            IOUtility.AssetDatabase.SaveScriptableObject (directoryPath, settings);

            Debug.LogFormat ("Creating {0} asset at {1}.", settings.name, directoryPath);
            #endif

            return settings;
        }
    }

    public interface IInitializable
    {
        void Init ();
        bool IsInitialized { get; }
    }
}