using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Instance.PlayBGM("Yoimaturi_no_kaze");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.Instance.StopBGM();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.Instance.ReplayBGM();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (AudioManager.Instance.IsPlayingBGM) AudioManager.Instance.PauseBGM();
            else AudioManager.Instance.UnPauseBGM();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AudioManager.Instance.PlayOneShot("Bomb");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            AudioManager.Instance.PlayOneShot("Bomb", 1.0f, 0.1f);
        }
    }
}
