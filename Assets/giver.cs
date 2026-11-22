using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class giver : MonoBehaviour
{
    public Item blub;
    public bool hasItem;
    public inventory inv;
    public UnityEvent eventer;
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && blub.itemID == inv.item.itemID)
        {
            Debug.Log("ммм, вкусно");
            //eventer.Invoke();
        }
    }
}
