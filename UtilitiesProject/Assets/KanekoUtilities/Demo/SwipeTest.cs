using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class SwipeTest : MonoBehaviour
{
    [SerializeField]
    Transform target = null;

    [SerializeField]
    float speed = 5.0f;
    Vector2 touchPos = Vector2.zero;

    void Start()
    {
        SwipeGetter.Instance.onTouchStart.AddListener((pos) =>
        {
            touchPos = pos;
        });

        SwipeGetter.Instance.onTouching.AddListener((pos) =>
        {
            var angle = (pos - touchPos).Angle();
            target.SetRotationY(KKUtilities.LerpRotation(target.eulerAngles.y, angle, Time.deltaTime * 12.0f));
        });
    }
}
