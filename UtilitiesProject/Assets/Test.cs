using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        MainSceneManager sceneManager = MainSceneManager.Instance;

        if(sceneManager == null)
        {
            Debug.Log("scene manager is null");
        }
        else
        {
            Debug.Log("scene manager is not null");
        }
    }
}
