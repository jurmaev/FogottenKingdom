using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundsController : MonoBehaviour
{
    [SerializeField] private float timeBetweenWalkSounds;
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource getDamageSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource attackSound;
    
    
    void Start()
    {
        if (walkSound.clip != null)
            StartCoroutine(StartPlayWalkSound()) ;
    }
    

    private IEnumerator StartPlayWalkSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWalkSounds + walkSound.clip.length + Random.Range(-1,1));
            walkSound.pitch = Random.Range(0.9f, 1.1f);
            walkSound.Play();
        }
    }

    public void PlayAttackSound()
    {
        if (attackSound.clip != null)
        {
            attackSound.pitch = Random.Range(0.9f, 1.1f);
            attackSound.Play();
        }
    }

    public void PlayGetDamageSound()
    {
        if (getDamageSound.clip != null)
        {
            getDamageSound.pitch = Random.Range(0.9f, 1.1f);
            getDamageSound.Play();
        }
    }

    public void PlayDeathSound()
    {
        if (deathSound.clip != null)
            deathSound.Play();
    }
}
