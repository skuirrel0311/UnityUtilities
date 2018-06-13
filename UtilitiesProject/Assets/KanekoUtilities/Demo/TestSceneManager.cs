using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //ParticleManager.Instance.PlayOneShot("Hanabi", new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f));
            //ParticleManager.Instance.PlayOneShot("Hanabi", new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f));
            MessageDisplayer.Instance.PlayMessage("aa", new Vector3(0.0f, 3.0f, 0.0f), 0.5f, 0.3f);

            MessageDisplayer.FontSettings fontSettings = MessageDisplayer.Instance.Default3DFontSettings;
            fontSettings.anchor = TextAnchor.UpperCenter;
            MessageDisplayer.Instance.PlayMessage("bb", new Vector3(0.0f, -3.0f, 0.0f), fontSettings, UIAnimationUtil.PopUpAnimation, UIAnimationUtil.DefaultHideAnimation, 0.3f, 0.3f, 0.1f);

        }
    }
}
