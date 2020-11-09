using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;
using UnityEngine.UI;

public class TestSceneManager : MonoBehaviour
{
    [SerializeField]
    AudioSource source = null;

    [SerializeField]
    Slider volumeSlider = null;
    [SerializeField]
    Slider pitchSlider = null;

    void Start()
    {
        volumeSlider.minValue = 0.0f;
        volumeSlider.maxValue = 1.0f;
        pitchSlider.minValue = 0.0f;
        pitchSlider.maxValue = 3.0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            source.volume = volumeSlider.value;
            source.pitch = pitchSlider.value;
            source.Play();
        }
    }
}
