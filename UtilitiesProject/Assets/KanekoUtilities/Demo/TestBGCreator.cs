using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TestBGCreator : MonoBehaviour
{
    [SerializeField]
    float cubeSize = 3.0f;
    [SerializeField]
    float minHeight = -9.5f;
    [SerializeField]
    float maxHeight = -5.0f;

    [SerializeField]
    ObjectPool cubePool = null;

    Rect rect;

    void Awake()
    {
        cubePool.GetOriginal.transform.localScale = new Vector3(cubeSize, maxHeight - minHeight, cubeSize);

        Init();
    }

    public void Init()
    {
        cubePool.ReturnAllInstance();

        float stageLength = 200.0f;
        rect = new Rect(Vector2.zero, new Vector2(50.0f, stageLength));
        rect.center = new Vector2(0.0f, (stageLength * 0.5f) - 20.0f);

        Generate(rect);
    }

    void Generate(Rect rect)
    {
        int h = (int)(rect.height / cubeSize);
        int w = (int)(rect.width / cubeSize);
        Vector3 min = rect.min.ToXZVector3();

        Vector3 generatePosition = min;
        generatePosition.x += cubeSize * 0.5f;
        generatePosition.z += cubeSize * 0.5f;

        GameObject cube;

        for (int i = 0; i < h; i++)
        {
            generatePosition.x = min.x;
            for (int j = 0; j < w; j++)
            {
                cube = cubePool.GetInstance().gameObject;
                generatePosition.y = Random.Range(minHeight, maxHeight);
                cube.transform.position = generatePosition;
                cube.SetActive(true);
                generatePosition.x += cubeSize;
            }

            generatePosition.z += cubeSize;
        }
    }
}
