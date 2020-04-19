using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audio_volume : MonoBehaviour {

    public AudioSource song;
    public string keyPref;
    float firstValuePref;

    private void Start()
    {
        song = GetComponent<AudioSource>();
        song.volume = PlayerPrefs.GetFloat(keyPref, song.volume);
        firstValuePref = PlayerPrefs.GetFloat(keyPref, song.volume);
    }

    private void Update()
    {
        if (firstValuePref != PlayerPrefs.GetFloat(keyPref, song.volume)) {
            song.volume = PlayerPrefs.GetFloat(keyPref, song.volume);
            firstValuePref = PlayerPrefs.GetFloat(keyPref, song.volume);
        }
    }

}
