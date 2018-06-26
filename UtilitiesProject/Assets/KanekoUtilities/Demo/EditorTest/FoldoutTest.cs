using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public enum TestType { A, B }

public class FoldoutTest : ScriptableObject
{
    [System.Serializable]
    private class SampleClass
    {
        [SerializeField]
        string hoge = "";
        [SerializeField]
        int fuga = 0;
    }

    [SerializeField]
    SampleClass sampleClass = null;
}
