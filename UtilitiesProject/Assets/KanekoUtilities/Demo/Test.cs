using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class Test : MonoBehaviour
{
    [SerializeField]
    GameObject cube = null;

    void Start()
    {
        var list = new List<Transform>();
        for(int i = 0 ;i < 360 ; i++)
        {
            var c = Instantiate(cube, transform);
            c.transform.rotation = Quaternion.Euler(i, 0.0f, 0.0f);
            
            list.Add(c.transform);
        }
        foreach(var c in list)
        {
            var up = c.up;
            Debug.Log("angle x = " + Mathf.Atan2(up.y, up.z) * Mathf.Rad2Deg);
        }
    }
}
