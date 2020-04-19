using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_loop : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicStart;

    private void Start()
    {
        musicSource.PlayOneShot(musicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
    }
}
