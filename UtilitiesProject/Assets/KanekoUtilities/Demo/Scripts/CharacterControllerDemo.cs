using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class CharacterControllerDemo : MonoBehaviour
{
    void Start()
    {
        Vector2 touchStartPos = Vector2.zero;
        SwipeGetter.Instance.onTouchStart.AddListener((pos) =>
        {
            touchStartPos = pos;
        });

        SwipeGetter.Instance.onTouching.AddListener((pos) =>
        {
            var vec = pos - touchStartPos;
            var angleY = KKUtilities.LerpRotation(transform.localEulerAngles.y, vec.Angle(), Time.deltaTime * 5.0f);
            transform.SetLocalRotationY(angleY);
            transform.Translate(vec.ToXZVector3() * Time.deltaTime * 0.02f, Space.World);
        });
    }
}
