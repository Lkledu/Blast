using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restart_Particle : MonoBehaviour
{
    public ParticleSystem particle;
    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (!particle.isPlaying) {
            particle.Clear();
            particle.Play();
        }
    }
}
