using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    bool active;

    [SerializeField]
    Dialog dialog = null;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (!active) dialog.Show();
            else dialog.Hide();

            active = !active;
        }
    }

}
