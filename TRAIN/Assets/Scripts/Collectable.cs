using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float interactableRadius;
    [SerializeField] private CollectableSO collectableSO;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;

    private bool _playerIsInRangeToCollect;
    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < interactableRadius)
        {
            _playerIsInRangeToCollect = true;
            spriteRenderer.color = Color.red;
        }
        else
        {
            _playerIsInRangeToCollect = false;
            spriteRenderer.color = new Color(255, 255, 255);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            if (InventoryController.Instance.HasAllInventoryElements()) return;

            InventoryController.Instance.AddNewElementToInventory(collectableSO);
            Destroy(gameObject);
        }
    }
    public bool IsPlayerInRange() => _playerIsInRangeToCollect;
}
