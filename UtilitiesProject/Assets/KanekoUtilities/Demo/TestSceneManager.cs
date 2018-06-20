using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanekoUtilities;

public class TestSceneManager : MonoBehaviour
{
    void Start()
    {
        if(LoginBonus.Instance.CanGetLoginBonus)
        {
            KKUtilities.Delay(1.0f, () => LoginBonus.Instance.ShowLoginBonusDialog(), this);
        }
    }

    [ContextMenu("DataReset")]
    public void ResetData()
    {
        MyPlayerPrefs.DeleteAll();
    }
}
