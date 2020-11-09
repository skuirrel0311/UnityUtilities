using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReLoadSceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
