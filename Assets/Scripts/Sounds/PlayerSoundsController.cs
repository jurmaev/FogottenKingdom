using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSoundsController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> walkSounds;
    [SerializeField] private AudioSource getDamageSound;
    [SerializeField] private AudioSource deathSound;
    private AudioSource currentWalkSound;


    private void Start()
    {
        currentWalkSound = walkSounds[0];
    }

    public void PlayWalkSound()
    {
        if (!currentWalkSound.isPlaying)
        {
            currentWalkSound = walkSounds[Random.Range(0, walkSounds.Count - 1)];
            currentWalkSound.pitch = Random.Range(0.9f, 1.1f);
            currentWalkSound.Play();
        }
    }
    
    public void PlayGetDamageSound()
    {
        getDamageSound.pitch = Random.Range(0.9f, 1.1f);
        getDamageSound.Play();
    }

    public void PlayDeathSound()
    {
        if (deathSound.clip != null)
            deathSound.Play();
    }
}
