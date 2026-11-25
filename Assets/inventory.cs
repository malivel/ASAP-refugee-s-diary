using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public Item item;
    public GameObject go;
    public SpriteRenderer sr;
    public AudioSource audioSource;
    public void add(GameObject items)
    {
        audioSource.Play();
        //if (item == null)
        //{
        go = items;
            item = go.GetComponent<Item>();
            sr.sprite = go.GetComponent<SpriteRenderer>().sprite;
        //}
    }
    public void give()
    {
        audioSource.Play();
        go = null;
        sr.sprite = null;
    }
}
