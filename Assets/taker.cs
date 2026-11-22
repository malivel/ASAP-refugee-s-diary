using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class taker : MonoBehaviour
{
    public GameObject blub;
    public UnityEvent eventer;
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            eventer.Invoke();
        }
    }
}