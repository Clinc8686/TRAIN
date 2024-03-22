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
    private List<CollectableSO> _collectableSOList;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        _collectableSOList = new List<CollectableSO>();
    }
    public void AddNewElementToInventory(CollectableSO collectableSO)
    {
        Debug.Log("Inventory");
        if (_index > inventoryContentSlots.Length - 1) return;

        if (_collectableSOList.Contains(collectableSO)) return;

        _collectableCounter++;
        inventoryContentSlots[_index++].sprite = collectableSO.collectableSprite;
        _collectableSOList.Add(collectableSO);
    }
    public bool HasAllInventoryElements() => _collectableCounter == inventoryContentSlots.Length;
}
