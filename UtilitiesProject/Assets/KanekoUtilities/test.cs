using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ParticleManager.I.PlayOneShot("Hanabi", new Vector3(0.0f, 1.5f, 0.0f));
            AudioManager.I.PlayOneShot("Bomb", new Vector3(0.0f, 1.5f, 0.0f));
        }
    }
}
