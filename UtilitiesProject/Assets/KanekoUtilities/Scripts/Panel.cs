using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//画面いっぱいに出るUI
[RequireComponent(typeof(Image))]
public class Panel : MonoBehaviour
{
    [SerializeField]
    bool activeOnAwake = false;

    Image panel;
    GameObject root;
    
    protected virtual void Awake()
    {
        panel = GetComponent<Image>();
        panel.enabled = activeOnAwake;
        root = transform.GetChild(0).gameObject;
        root.SetActive(activeOnAwake);
    }

    public virtual void Activate()
    {
        panel.enabled = true;
        root.SetActive(true);
    }
    public virtual void Deactivate()
    {
        panel.enabled = false;
        root.SetActive(false);
    }
}
