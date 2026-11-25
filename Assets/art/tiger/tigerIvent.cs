using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class tigerIvent : MonoBehaviour
{
    public UnityEvent eventer;
    public UnityEvent dead;
    public SpriteRenderer lols;
    public Sprite sp2;
    private bool sell = true;
    public saveZone savez;
    private bool save;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D lol)
    { 
        if (sell)
        {
            sell = false;
            audioSource.Play();
            StartCoroutine(Timer());
        }
        
    }
    IEnumerator Timer()
    {
        lols.enabled = true;
        
        yield return new WaitForSeconds(5f);
        
        save = savez.save;
        if (save)
        {
            eventer.Invoke();
            lols.sprite = sp2;
            yield return new WaitForSeconds(4f);
            lols.enabled = false;
        }
        else
        {
            videoPlayer.Play();
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("deathEnd");
        }
    }

}
