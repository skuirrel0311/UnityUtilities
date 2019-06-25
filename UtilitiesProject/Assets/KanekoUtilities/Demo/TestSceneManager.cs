using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    [SerializeField, OneLine.OneLine]
    GameObjectWeightedTableElement[] elements = null;
    WeightedTable<GameObject> gameoObjectWeightedTable;

    void Start()
    {
        gameoObjectWeightedTable = new WeightedTable<GameObject>(elements);
    }

    void Update()
    {
        Debug.Log("random value = " + gameoObjectWeightedTable.GetData());
    }

}
