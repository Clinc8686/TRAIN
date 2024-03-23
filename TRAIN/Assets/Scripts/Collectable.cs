using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public float interactableRadius = 3;
    [SerializeField] private CollectableSO collectableSO;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;
    public float distanceToPlayer;

    private bool _playerIsInRangeToCollect;

    private void Start()
    {
        if (interactableRadius <= 0)
        {
            Debug.LogWarning("interactableRadius not large enough");  
        }
    }

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
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
