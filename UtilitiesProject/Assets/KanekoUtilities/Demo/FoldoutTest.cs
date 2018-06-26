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


public abstract class Monster
{
    public abstract string ID { get; }
}

public class Warrior : Monster
{
    public override string ID
    {
        get
        {
            return "Warrior";
        }
    }
}

public class Witch : Monster
{
    public override string ID
    {
        get
        {
            return "Witch";
        }
    }
}

public class Dragon : Monster
{
    public override string ID
    {
        get
        {
            return "Dragon";
        }
    }
}
