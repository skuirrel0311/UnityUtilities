using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    /// <summary>
    /// Monobehaviourを継承しないクラスにも使えるようにinterfaceで実装
    /// </summary>
    public interface IObjectPool<T>
    {
        T GetOriginal { get; }
        T GetInstance();
        void ReturnInstance(T instance);
    }

    public class ObjectPool<T> : MonoBehaviour, IObjectPool<T>
        where T : PoolMonoBehaviour
    {
        [SerializeField]
        int maxCount = 0;
        [SerializeField]
        float removeIntervalTime = 10.0f;
        //プレハブを入れなかったら子を使用する
        [SerializeField]
        protected T original = null;
        
        public List<T> InstanceList { get; protected set; }
        public List<T> ActiveInstanceList { get; protected set; }
        Queue<T> deactiveInstanceQueue = new Queue<T>();

        void Awake()
        {
            InstanceList = new List<T>();
            ActiveInstanceList = new List<T>();
        }

        void OnEnable()
        {
            StartCoroutine(RemoveChecker());
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }

        IEnumerator RemoveChecker()
        {
            WaitForSeconds wait = new WaitForSeconds(removeIntervalTime);
            while (true)
            {
                yield return wait;
                RemoveCheck();
            }
        }

        //MaxCountを超えてオブジェクトが生成されている場合は使用していないオブジェクトを破棄する
        void RemoveCheck()
        {
            T removeInstance;
            while(0 < deactiveInstanceQueue.Count)
            {
                removeInstance = deactiveInstanceQueue.Dequeue();
                InstanceList.Remove(removeInstance);
                Destroy(removeInstance);

                if (InstanceList.Count <= maxCount) break;
            }
        }
        
        public T GetInstance()
        {
            T instance;
            if (deactiveInstanceQueue.Count >= 1)
            {
                instance = deactiveInstanceQueue.Dequeue();
                ActiveInstanceList.Add(instance);
                return instance;
            }

            //足りない場合は生成する
            instance = Instantiate(GetOriginal, transform);
            InstanceList.Add(instance);
            return instance;
        }

        public void ReturnInstance(T instance)
        {
            instance.gameObject.SetActive(false);
            deactiveInstanceQueue.Enqueue(instance);
            ActiveInstanceList.Remove(instance);
        }

        public virtual T GetOriginal
        {
            get
            {
                if (original != null) return original;
                original = transform.GetChild(0).GetComponent<T>();

                return original;
            }
        }
    }

    public class ObjectPool : ObjectPool<PoolMonoBehaviour> { }
}
