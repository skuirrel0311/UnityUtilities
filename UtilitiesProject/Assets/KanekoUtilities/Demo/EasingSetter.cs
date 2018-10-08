using System;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class EasingSetter : MonoBehaviour
{
    [SerializeField]
    MovableImage imageOriginal = null;


    [ContextMenu("SetEasing")]
    public void SetEasing()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
        int count = Enum.GetNames(typeof(EaseType)).Length;

        for (int i = 0; i < count; i++)
        {
            MovableImage image = Instantiate(imageOriginal, transform);
            image.name = ((EaseType)i).ToString();
            image.gameObject.SetActive(true);
            image.ease = (EaseType)i;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            MovableImage[] images = GetComponentsInChildren<MovableImage>();
            Vector2 pos;
            for (int i = 0; i < images.Length; i++)
            {
                pos = images[i].Image.rectTransform.anchoredPosition;
                pos.x = 25.0f;
                images[i].Image.rectTransform.anchoredPosition = pos;
            }
        }
    }
}
