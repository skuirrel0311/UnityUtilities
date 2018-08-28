using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    [SerializeField]
    int count = 10;

    float t;
    float interval = 0.2f;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            t += Time.deltaTime;
            if (t < interval) return;

            t = 0.0f;

            for (int i = 0; i < count; i++)
            {
                AudioManager.Instance.PlayOneShot("Bomb");
            }
        }
    }
}
