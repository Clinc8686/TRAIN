using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public float interactableRadius = 3;
    [SerializeField] private CollectableSO collectableSO;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;
    [SerializeField] private Color highlightedColor;
    [SerializeField] private Color normalColor;
    
    private float _distanceToPlayer;
    private bool _playerIsInRangeToCollect;
    private void Update()
    {
        _distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (_distanceToPlayer <= interactableRadius) _playerIsInRangeToCollect = true;
        else _playerIsInRangeToCollect = false;

        if(_playerIsInRangeToCollect)
        {
            Debug.Log("Hier");
            spriteRenderer.material.color = highlightedColor;
        }
        else
        {
            spriteRenderer.material.color = normalColor;
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
    public float GetDistanceToPlayer() => _distanceToPlayer;
}
