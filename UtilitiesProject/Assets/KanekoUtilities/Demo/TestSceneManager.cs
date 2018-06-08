using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    [SerializeField]
    NumberText proText = null;
    [SerializeField]
    NumberText unityText = null;

    void Start()
    {
        proText.SetValue(10);
        unityText.SetValue(50);
    }
}
