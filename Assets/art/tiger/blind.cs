using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blind : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public void bild()
    {
        spriteRenderer.sprite = null;
    }
}
