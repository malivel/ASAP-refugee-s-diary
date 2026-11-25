using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    public Animator anim;
    public void love()
    {
        anim.SetBool("love", true);
    }
}
