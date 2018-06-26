using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class DialogTestSceneManager : MonoBehaviour
{
    [SerializeField]
    MonsterContainer container = null;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            DialogDisplayer.Instance.ShowOkCancelDialog(() =>
            {
                Debug.Log("on ok!");
                DialogDisplayer.Instance.HideDialog();
            },null);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            DialogDisplayer.Instance.HideDialog();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            container.ShowName();
        }
    }
}
