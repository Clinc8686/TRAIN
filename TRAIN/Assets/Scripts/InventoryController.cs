using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }

    [SerializeField] private Image[] inventoryContentSlots;

    private int _index = 0;
    private int _collectableCounter = 0;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
    public void AddNewElementToInventory(CollectableSO collectableSO)
    {
        if (_index > inventoryContentSlots.Length - 1) return;

        _collectableCounter++;
        inventoryContentSlots[_index++].sprite = collectableSO.collectableSprite;
    }
    public bool HasAllInventoryElements() => _collectableCounter == inventoryContentSlots.Length;
}
