using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class fisheat : MonoBehaviour
{
    public Animator animator;
    public AudioClip splashSound;
    [Range(0f, 1f)] public float volume = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void fisher()
    {
        animator.SetBool("eat", true);
    }


    public void PlaySplash()
    {
        if (splashSound != null)
            audioSource.PlayOneShot(splashSound, volume);
    }
}