using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = UtilsClass.Utils.GetSortingOrder(transform.position, -10);
    }
}
