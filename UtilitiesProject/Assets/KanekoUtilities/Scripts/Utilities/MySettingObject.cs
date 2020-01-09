using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperCasual;

namespace KanekoUtilities
{
	public abstract class MySettingsObject<T> : ScriptableObject where T : ScriptableObject, IInitializable
	{
		public const string SettingsDirectory = "Settings";

		[System.NonSerialized] private bool isInitialized = false;
		public bool IsInitialized
		{
			get { return isInitialized; }
			protected set { isInitialized = value; }
		}

		private static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = Load();
#if UNITY_EDITOR
                    if (Application.isPlaying) {
                        instance.Init ();
                    }
#else
					instance.Init();
#endif
				}
				return instance;
			}
		}

		public static T Load()
		{
			return MyAssetStore.Instance.GetAsset<T>(typeof(T).Name, "Settings/");
        }
	}

	public interface IInitializable
	{
		void Init();
		bool IsInitialized { get; }
	}
}