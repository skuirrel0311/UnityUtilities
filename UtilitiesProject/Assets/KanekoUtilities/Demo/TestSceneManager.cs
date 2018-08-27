using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    [SerializeField]
    int count = 10;
    [SerializeField]
    int max = 50;

    float t;

    float interval = 0.2f;

    [SerializeField]
    SoundEffectPool soundEffectPool = null;
    [SerializeField]
    AudioClip clip = null;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            t += Time.deltaTime;
            if (t < interval) return;

            t = 0.0f;

            for (int i = 0; i < count; i++)
            {
                if (soundEffectPool.ActiveInstanceList.Count > max)
                {
                    return;
                }

                SoundEffect effect = soundEffectPool.GetInstance();
                effect.Play(clip, () => soundEffectPool.ReturnInstance(effect));
            }
        }
    }
}
