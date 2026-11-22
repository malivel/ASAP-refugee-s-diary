using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabanAI : MonoBehaviour
{
    public int ai = 0;
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("add", 0);
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            ai = 1;
            anim.SetInteger("add", ai);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            ai = 0;
            anim.SetInteger("add", ai);
        }
    }

}
