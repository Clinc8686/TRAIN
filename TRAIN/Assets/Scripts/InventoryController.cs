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

    //Collectables
    public bool _hasSun;
    public bool _hasBubads;
    public bool _hasTicket;
    public bool _hasTrain;
    public bool _hasSamen;
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

        _collectableSOList = new List<CollectableSO>();
    }
    public void AddNewElementToInventory(CollectableSO collectableSO)
    {
        if (_index > inventoryContentSlots.Length - 1) return;

        if (_collectableSOList.Contains(collectableSO)) return;

        if (collectableSO.collectableName.Equals("Train")) _hasTrain = true;
        if (collectableSO.collectableName.Equals("Bubads")) _hasBubads = true;
        if (collectableSO.collectableName.Equals("Sonne")) _hasSun = true;
        if (collectableSO.collectableName.Equals("Fahrkarte")) _hasTicket = true;
        if (collectableSO.collectableName.Equals("Samen")) _hasSamen= true;

        _collectableCounter++;
        inventoryContentSlots[_index++].sprite = collectableSO.collectableSprite;
        _collectableSOList.Add(collectableSO);
    }
    public bool HasTrain() => _hasTrain;
    public bool HasBubads() => _hasBubads;
    public bool HasSun() => _hasSun;
    public bool HasTicket() => _hasTicket;
    public bool HasSamen() => _hasSamen;
    public bool HasAllInventoryElements() => _collectableCounter == inventoryContentSlots.Length;
}
