using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class muhaha : MonoBehaviour
{
    public inventory inv;
    public UnityEvent eventer;

    private AudioSource buzz;

    void Awake()
    {
        buzz = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D lol)
    {
        if (inv.item != null)
        {
            if (inv.item.itemID == 0)
            {
                if (buzz != null) buzz.Stop();

                eventer.Invoke();

                // если мошка уничтожаетс€ Ч звук уже остановлен
                // Destroy(gameObject); // раскомментируй, если нужно
            }
        }
    }
}