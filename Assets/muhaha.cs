using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class muhaha : MonoBehaviour
{
    public inventory inv;
    public UnityEvent eventer;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D lol)
    {
        if (inv.item != null)
        {
            if (inv.item.itemID == 0)
            {
                eventer.Invoke();
            }
        }
        
    }
}
