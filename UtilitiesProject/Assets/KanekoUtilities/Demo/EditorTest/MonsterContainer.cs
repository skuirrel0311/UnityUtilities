using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { Warrior, Witch, Dragon }

public class MonsterContainer : ScriptableObject
{
    [SerializeField]
    Warrior warrior = null;

    [SerializeField]
    Witch witch = null;

    [SerializeField]
    Dragon dragon = null;

    public MonsterType monsterType;

    public void ShowName()
    {
        switch (monsterType)
        {
            case MonsterType.Warrior:
                Debug.Log("warrior");
                break;
            case MonsterType.Witch:
                Debug.Log("witch");
                break;
            case MonsterType.Dragon:
                Debug.Log("dragon");
                break;
        }
    }
}

public abstract class Monster
{
    public int startValue;
    public int endValue;
}

[System.Serializable]
public class Warrior : Monster
{
    public int power;
}

[System.Serializable]
public class Witch : Monster
{
    public int magicPower;
}

[System.Serializable]
public class Dragon : Monster
{
    public int flyPower;
}