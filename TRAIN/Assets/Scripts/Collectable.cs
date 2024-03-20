using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableSO collectableSO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            if (InventoryController.Instance.HasAllInventoryElements()) return;

            InventoryController.Instance.AddNewElementToInventory(collectableSO);
            Destroy(gameObject);
        }
    }
}
