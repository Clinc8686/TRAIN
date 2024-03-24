using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CollectableSO : ScriptableObject
{
    public Sprite collectableSprite;
    public InventoryController.CollectableItem collectableItem;

    public CollectableSO(Sprite sprite, InventoryController.CollectableItem item)
    {
        this.collectableItem = item;
        this.collectableSprite = sprite;
    }
}
