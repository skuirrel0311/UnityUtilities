using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public interface IObjectPooler<T>
    {
        int MaxCount { get; }
        float RemoveIntervalTime { get; }
        T GetOriginal { get; }
        T GetInstance();
    }

    public class ObjectPooler : MonoBehaviour, IObjectPooler<PoolMonoBehaviour>
    {
        [SerializeField]
        int maxCount = 0;
        [SerializeField]
        float removeIntervalTime = 10.0f;

        public int MaxCount { get { return maxCount; } }
        public float RemoveIntervalTime { get { return removeIntervalTime; } }
        public List<PoolMonoBehaviour> InstanceList { get { return instanceList; } }

        protected PoolMonoBehaviour original;
        List<PoolMonoBehaviour> instanceList = new List<PoolMonoBehaviour>();

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
            WaitForSeconds wait = new WaitForSeconds(RemoveIntervalTime);
            while (true)
            {
                yield return wait;
                RemoveCheck();
            }
        }

        //MaxCountを超えてオブジェクトが生成されている場合は使用していないオブジェクトを破棄する
        void RemoveCheck()
        {
            RemoveNullObject();
            if (instanceList.Count <= MaxCount) return;

            for (int i = instanceList.Count - 1; i > 0; i--)
            {
                if (instanceList[i].IsActive) continue;

                PoolMonoBehaviour removeObject = instanceList[i];
                instanceList.Remove(removeObject);
                Destroy(removeObject.gameObject);
                //MaxCountよりも少なくなったらやめてもいいはず
                if (instanceList.Count <= MaxCount) return;
            }
        }

        void RemoveNullObject()
        {
            for (int i = instanceList.Count - 1; i > 0; i--)
            {
                if (instanceList[i] == null) instanceList.RemoveAt(i);
            }
        }

        public PoolMonoBehaviour GetInstance()
        {
            RemoveNullObject();

            //Activeが切れているのがあればそれを優先して使う
            for (int i = 0; i < instanceList.Count; i++)
            {
                if (!instanceList[i].IsActive)
                {
                    instanceList[i].gameObject.SetActive(true);
                    return instanceList[i];
                }
            }

            //足りない場合はとりあえず生成する
            PoolMonoBehaviour instance = Instantiate(GetOriginal, transform);
            instanceList.Add(instance);

            instance.gameObject.SetActive(true);
            return instance;
        }

        public virtual PoolMonoBehaviour GetOriginal
        {
            get
            {
                if (original != null) return original;
                original = transform.GetChild(0).GetComponent<PoolMonoBehaviour>();

                return original;
            }
        }
    }
}
