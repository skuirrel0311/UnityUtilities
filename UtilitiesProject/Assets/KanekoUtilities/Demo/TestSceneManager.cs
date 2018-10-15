using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanekoUtilities;
using DG.Tweening;

public class TestSceneManager : MonoBehaviour
{
    void Start()
    {
        SwipeGetter.Instance.onTap.AddListener((pos) =>
        {
#if IMPORT_HYPERCOMMON
            MessageDisplayer.Instance.ShowMessage("define", Vector2.zero);
#else
            MessageDisplayer.Instance.ShowMessage("not define", Vector2.zero);
#endif
        });
    }
}
