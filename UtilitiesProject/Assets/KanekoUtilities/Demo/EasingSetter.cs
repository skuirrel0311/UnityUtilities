using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class EasingSetter : MonoBehaviour
{
    [ContextMenu("SetEasing")]
    public void SetEasing()
    {
        MovableImage[] images = GetComponentsInChildren<MovableImage>();

        for(int i = 0;i< images.Length;i++)
        {
            images[i].gameObject.name = ((EaseType)i).ToString();
            images[i].ease = (EaseType)i;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            MovableImage[] images = GetComponentsInChildren<MovableImage>();
            Vector2 pos;
            for (int i = 0; i < images.Length; i++)
            {
                pos = images[i].Image.rectTransform.anchoredPosition;
                pos.x = -375.0f;
                images[i].Image.rectTransform.anchoredPosition = pos;
            }
        }
    }
}
