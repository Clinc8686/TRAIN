using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eule : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spritesArray;

    private void Awake()
    {
        spriteRenderer.sprite = spritesArray[Random.Range(0, spritesArray.Length)];
    }
}
