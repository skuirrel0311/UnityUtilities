using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

[RequireComponent(typeof(ObjectPooler))]
public class RegularIntervalObjectGenerator :  MonoBehaviour
{
    [SerializeField]
    protected Vector3 objectDistance = Vector3.zero;
    public Vector3 ObjectDistance { get { return objectDistance; } }
    [SerializeField]
    protected int startGenerateNum = 10;
    protected Vector3 startPosition;

    public ObjectPooler Pooler { get; private set; }
    protected List<PoolMonoBehaviour> objectList = new List<PoolMonoBehaviour>();
    public List<PoolMonoBehaviour> ObjectList { get { return objectList; } }
    protected int totalObjectCount;
    public int TotalObjectCount { get { return totalObjectCount; } }

    protected virtual void Awake()
    {
        startPosition = transform.position;
        Pooler = GetComponent<ObjectPooler>();
    }

    protected virtual void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        for(int i = 0;i < ObjectList.Count;i++)
        {
            ObjectList[i].gameObject.SetActive(false);
        }

        ObjectList.Clear();
        totalObjectCount = 0;

        for (int i = 0; i < startGenerateNum; i++)
        {
            Generate();
        }
    }

    public PoolMonoBehaviour Generate()
    {
        PoolMonoBehaviour obj = Pooler.GetInstance();

        obj.transform.position = startPosition + objectDistance * totalObjectCount;

        objectList.Add(obj);
        totalObjectCount++;
        return obj;
    }
}
