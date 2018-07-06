using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class DialogTestSceneManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            DialogDisplayer.Instance.ShowOkCancelDialog(null, null);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            DialogDisplayer.Instance.HideDialog();
        }
    }
}
