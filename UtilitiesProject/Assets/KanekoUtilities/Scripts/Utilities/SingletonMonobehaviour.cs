using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KanekoUtilities
{
    public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        GameObject prefab = Resources.Load<GameObject>("SingletonMonobehaviour/" + typeof(T).Name);

                        if(prefab == null)
                        {
                            Debug.Log("not load prefab");
                        }

                        Instantiate(Instantiate(prefab));
                    }
                }
                return instance;
            }
            protected set
            {
                instance = value;
            }
        }
        [SerializeField]
        bool dontDestroyOnLoad = false;

        protected virtual void Awake()
        {
            Inisialize();
            SceneManager.sceneLoaded += WasLoaded;
        }

        protected virtual void WasLoaded(Scene sceneName, LoadSceneMode sceneMode)
        {
            Inisialize();
        }

        void Inisialize()
        {
            List<T> instances = new List<T>();
            instances.AddRange((T[])FindObjectsOfType(typeof(T)));

            if (Instance == null) Instance = instances[0];
            instances.Remove(Instance);

            if (instances.Count == 0) return;
            //あぶれ者のinstanceはデストロイ 
            foreach (T t in instances) Destroy(t.gameObject);
        }

        protected virtual void Start()
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            SceneManager.sceneLoaded -= WasLoaded;
            Instance = null;
        }
    }
}