using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class Test : MonoBehaviour
{
    [SerializeField]
    UGUIButton button = null;

    void Awake()
    {
        button.OnClickEvent.AddListener(() =>
        {
            ParticleManager.Instance.PlayOneShot("PoppingEff", Vector3.zero);
        });
    }
}
