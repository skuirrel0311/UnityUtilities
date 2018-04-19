using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

//オブジェクトを一定間隔に生成する人
//使うときは空のクラスを定義して明示的に生成するオブジェクトの型を指定してください
public class RegularIntervalObjectGenerator<T> : ObjectPool<T>
    where T : PoolMonoBehaviour
{
    [SerializeField]
    protected Vector3 objectDistance = Vector3.zero;
    public Vector3 ObjectDistance { get { return objectDistance; } }
    [SerializeField]
    protected int startGenerateNum = 10;
    protected Vector3 startPosition;
    protected int totalObjectCount;
    public int TotalObjectCount { get { return totalObjectCount; } }

    protected virtual void Awake()
    {
        startPosition = transform.position;
    }

    protected virtual void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        for (int i = 0; i < ActiveInstanceList.Count; i++)
        {
            ReturnInstance(ActiveInstanceList[i]);
        }

        totalObjectCount = 0;

        for (int i = 0; i < startGenerateNum; i++)
        {
            Generate();
        }
    }

    public T Generate()
    {
        T obj = GetInstance();

        obj.transform.position = startPosition + objectDistance * totalObjectCount;

        totalObjectCount++;
        return obj;
    }
}
