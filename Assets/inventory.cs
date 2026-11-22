using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public Item item;

    public void add(Item items)
    {
        if (item == null)
        {
            item = items;
        }
    }
}
