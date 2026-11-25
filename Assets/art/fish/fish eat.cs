using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fisheat : MonoBehaviour
{
    public Animator animator;
    public void fisher()
    {
        animator.SetBool("eat",true);
    }
}
