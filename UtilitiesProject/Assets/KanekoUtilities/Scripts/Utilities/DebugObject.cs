using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class DebugObject : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(Debug.isDebugBuild);
    }
}
