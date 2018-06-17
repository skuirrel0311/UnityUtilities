using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    [SerializeField]
    Transform emitter = null;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ParticleManager.Instance.PlayOneShot("Hanabi", emitter.position);
            ParticleManager.Instance.PlayOneShot("Hanabi", emitter.position + Vector3.up);
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"), 0.0f);

        emitter.position += movement * 0.3f;
    }
}
