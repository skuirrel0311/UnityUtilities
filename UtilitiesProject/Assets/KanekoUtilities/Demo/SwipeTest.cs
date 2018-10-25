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

    void Start()
    {
        SwipeGetter.Instance.onSwipe.AddListener((vec) =>
        {
            target.AddPositoinX(vec.x * Time.deltaTime * speed);
        });
    }
}
