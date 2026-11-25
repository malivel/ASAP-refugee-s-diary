using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveZone : MonoBehaviour
{
    public bool save = false;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D fuf)
    {
        save = true;
    }
    void OnTriggerExit2D(Collider2D fuf)
    {
        save = false;
    }
}
