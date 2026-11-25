using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tigerEnd : MonoBehaviour
{
    public saveZone savez;
    private bool save;
    public void heh(bool save)
    {
        save = savez.save;
        if (save)
        {
            this.enabled = false;
        }
        else
        {

        }
    }
}
