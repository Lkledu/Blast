using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onomatopeia_sound : MonoBehaviour
{
    public string keyPref;
    public AudioSource sfx;

    private void OnEnable()
    {
        
        sfx.volume = PlayerPrefs.GetFloat(keyPref, 1);
        sfx.Play();

    }
}
