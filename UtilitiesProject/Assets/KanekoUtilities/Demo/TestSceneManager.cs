using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    void Start()
    {

    }

    IEnumerator Hoge()
    {
        yield return KKUtilities.WaitAction(SwipeGetter.Instance.onTap);

        Debug.Log("on tap");
    }

    [ContextMenu("DataReset")]
    public void ResetData()
    {
        MyPlayerPrefs.DeleteAll();
    }
}
