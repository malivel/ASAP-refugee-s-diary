using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fcaban : MonoBehaviour
{
    public giver g;
    public Animator anim;
    public SpriteRenderer sp;
    public void huh()
    {
        g.enabled = false;
        anim.SetInteger("add", 2);
        StartCoroutine(RepeatAction());
    }

    IEnumerator RepeatAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            sp.enabled = true;
            Destroy(gameObject);

        }
    }

}
