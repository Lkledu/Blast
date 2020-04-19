using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_volume : MonoBehaviour {

    public string keyPref;
    Slider sliderMusic;
    float volume = 1f;

    private void Start()
    {
        sliderMusic = GetComponent<Slider>();
        sliderMusic.value = PlayerPrefs.GetFloat(keyPref, volume);
    }

    public void SaveSliderValue()
    {
        PlayerPrefs.SetFloat(keyPref, sliderMusic.value);
    }
}
