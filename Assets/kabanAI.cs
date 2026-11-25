using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabanAI : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioClip musicClips;
    public int ai = 0;
    public Animator anim;
    public AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("add", 0);
        audioSource.Play();
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            ai = 1;
            anim.SetInteger("add", ai);
            audioSource.clip = musicClips;
            audioSource.Play();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            audioSource.Play();
            ai = 0;
            audioSource.clip = musicClip;
            anim.SetInteger("add", ai);
        }
    }


}
