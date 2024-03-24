using System;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform interactionSignTransform;
    
    [Tooltip("Dialog to be displayed at the beginning of the scene.")] 
    [SerializeField] private string[] onSceneStartingDialog;
    [Tooltip("Dialog to be displayed if the player arrives without the required item.")] 
    [SerializeField] private string[] dialogWithoutItem;
    [Tooltip("Dialog to be displayed when the player arrives with the required item.")] 
    [SerializeField] private string[] dialogWithItem;
    [Tooltip("Dialog to be displayed when the player receives a new item.")] 
    [SerializeField] private string[] collectDialog;
    [Tooltip("Distance at which interaction is possible.")] 
    [SerializeField] private float interactionDistance = 10f;
    [Tooltip("Item that is required by the character.")] 
    [SerializeField] private InventoryController.CollectableItem itemToCollect;
    [Tooltip("Item that the character gives to the player.")] 
    [SerializeField] private InventoryController.CollectableItem itemToReceive;
    [Tooltip("Look without the required item")] 
    [SerializeField] private Sprite _spriteWithoutItem;
    [Tooltip("Look with the required item")] 
    [SerializeField] private Sprite _spriteWithItem;

    private Player _player;
    private InventoryController _inventoryController;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _player = Player.Instance;
        _inventoryController = InventoryController.Instance;
        _spriteRenderer = GetComponentInChild<SpriteRenderer>();
    }

    private void Start()
    {
        if (onSceneStartingDialog != null)
        {
            DialogController.Instance.WriteText(onSceneStartingDialog, _player);
        }
        
        CheckSprite();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        if(distanceToPlayer <= interactionDistance)
        {
            interactionSignTransform.gameObject.SetActive(true);
        }
        else
        {
            if(interactionSignTransform.gameObject.activeInHierarchy) 
               interactionSignTransform.gameObject.SetActive(false);
        }
    }

    //Method that searches in child objects for the given object type and returns it
    private T GetComponentInChild<T>() where T : Component
    {
        T component = GetComponent<T>();
        if (component != null) return component;
        foreach (Transform child in transform)
        {
            component = child.GetComponent<T>();
            if (component != null) return component;
        }
        return null;
    }

    public void Interact()
    {
        float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (distanceToPlayer > interactionDistance) return;

        CheckCollectDialog();
        CheckItemDialog();
        CheckSprite();
    }

    //Method displays dialog if the player gets new item from the character
    private void CheckCollectDialog()
    {
        if (itemToReceive != InventoryController.CollectableItem.Default && _inventoryController.PlayerHasItem(itemToReceive) != true)
        {
            if (collectDialog != null)
            {
                if (DialogController.Instance.IsWriting()) return;
                DialogController.Instance.ResetIsWritingState();
                DialogController.Instance.WriteText(collectDialog, _player);
                //InventoryController.Instance.AddNewElementToInventory(collectableSO);
                InventoryController.Instance.AddNewElementToInventory(itemToReceive);
            }
        }
    }

    //Method displays dialog depending on whether the player has the required item
    private void CheckItemDialog()
    {
        if (itemToCollect != InventoryController.CollectableItem.Default)
        {
            if (_inventoryController.PlayerHasItem(itemToCollect))
            {
                if (DialogController.Instance.IsWriting()) return;
                DialogController.Instance.ResetIsWritingState();
                DialogController.Instance.WriteText(dialogWithItem, _player);
            }
        }
        else
        {
            if (DialogController.Instance.IsWriting()) return;
            DialogController.Instance.ResetIsWritingState();
            DialogController.Instance.WriteText(dialogWithoutItem, _player);
        }
    }
    
    //Method changes the sprite depending on whether the player has the required item
    private void CheckSprite()
    {
        if (_inventoryController.PlayerHasItem(itemToCollect))
        {
            if (_spriteWithItem != null)
            {
                _spriteRenderer.sprite = _spriteWithItem;
            }
        }
        else
        {
            if (_spriteWithoutItem != null)
            {
                _spriteRenderer.sprite = _spriteWithoutItem;
            }
        }
    }
}
