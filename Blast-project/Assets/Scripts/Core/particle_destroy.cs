using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_destroy : MonoBehaviour
{
    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!particle.isPlaying) { Destroy(gameObject); }
    }
}
