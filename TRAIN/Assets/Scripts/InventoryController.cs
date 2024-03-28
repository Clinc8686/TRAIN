using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }

    [SerializeField] private Image[] inventoryContentSlots;
    [SerializeField] private CollectableItem[] collectableItems;
    [SerializeField] private Sprite[] collectableSprites;
    //CollectableSO from ScriptableObjects
    
    private int _index = 0;
    private int _collectableCounter = 0;
    private List<CollectableSO> _collectedItems;
    

    //Collectables
    public bool _hasSun;
    public bool _hasBubads;
    public bool _hasTicket;
    public bool _hasTrain;
    public bool _hasSamen;
    public enum CollectableItem
    {
        Default,
        Sun,
        Bubads,
        Ticket,
        Train,
        Samen
    }
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("InventoryController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _collectedItems = new List<CollectableSO>();
        for (int i = 0; i < collectableItems.Length; i++)
        {
            _collectedItems.Add(new CollectableSO(collectableSprites[i], collectableItems[i]));
        }
    }

    public void AddNewElementToInventory(CollectableItem item)
    {
        if (_index > inventoryContentSlots.Length - 1) return;

        foreach (CollectableSO collectableSo in _collectedItems)
        {
            if (collectableSo.collectableItem == item)
            {
                _collectableCounter++;
                inventoryContentSlots[_index++].sprite = collectableSo.collectableSprite;
                _collectedItems.Add(collectableSo);
            }
        }
    }
    public void AddNewElementToInventory(CollectableSO collectableSO)
    {
        if (_index > inventoryContentSlots.Length - 1) return;

        if (_collectedItems.Contains(collectableSO)) return;

        if ((collectableSO.collectableItem == CollectableItem.Train)) _hasTrain = true;
        if ((collectableSO.collectableItem == CollectableItem.Bubads)) _hasBubads = true;
        if ((collectableSO.collectableItem == CollectableItem.Sun)) _hasSun = true;
        if ((collectableSO.collectableItem == CollectableItem.Ticket)) _hasTicket = true;
        if ((collectableSO.collectableItem == CollectableItem.Train)) _hasSamen = true;

        _collectableCounter++;
        inventoryContentSlots[_index++].sprite = collectableSO.collectableSprite;
        _collectedItems.Add(collectableSO);
    }
    public bool HasTrain() => _hasTrain;
    public bool HasBubads() => _hasBubads;
    public bool HasSun() => _hasSun;
    public bool HasTicket() => _hasTicket;
    public bool HasSamen() => _hasSamen;
    public bool HasAllInventoryElements() => _collectableCounter == inventoryContentSlots.Length;
    
    //Checks if the player has the item in his inventory
    public bool PlayerHasItem(CollectableItem item)
    {
        switch (item)
        {
            case CollectableItem.Bubads:
                return _hasBubads;
            case CollectableItem.Sun:
                return _hasSun;
            case CollectableItem.Samen:
                return _hasSamen;
            case CollectableItem.Ticket:
                return _hasTicket;
            case CollectableItem.Train:
                return _hasTrain;
            default:
                Debug.LogWarning("Item is not in Inventory Controller");
                return false;
        }
    }
}
