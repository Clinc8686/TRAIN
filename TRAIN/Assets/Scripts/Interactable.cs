using System;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform interactionSignTransform;
    
    [Tooltip("Dialog to be displayed at the beginning of the scene.")] 
    [SerializeField] private string[] onSceneStartingDialog;
    [Tooltip("Dialog to be displayed when the player arrives the character for the first time.")] 
    [SerializeField] private string[] firstContactDialog;
    [Tooltip("Dialog to be displayed when the player arrives the character repeatedly.")] 
    [SerializeField] private string[] returningDialog;
    [Tooltip("Dialog to be displayed when the player arrives with the required item.")] 
    [SerializeField] private string[] collectDialog;
    [Tooltip("Distance at which interaction is possible.")] 
    [SerializeField] private float interactionDistance = 10f;
    [Tooltip("Item that is required by the character.")] 
    [SerializeField] private InventoryController.CollectableItem itemToCollect;
    [Tooltip("Item that the character gives to the player.")] 
    [SerializeField] private InventoryController.CollectableItem itemToReceive;
    [Tooltip("Character look without the required item")] 
    [SerializeField] private Sprite _spriteWithoutItem;
    [Tooltip("Character look with the required item")] 
    [SerializeField] private Sprite _spriteWithItem;

    private Player _player;
    private InventoryController _inventoryController;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _player = Player.Instance;
        _inventoryController = InventoryController.Instance;
        _spriteRenderer = GetComponentInChild<SpriteRenderer>();
        PlayerPrefs.SetInt("Conductor", 0);
    }

    private void Start()
    {
        if (onSceneStartingDialog != null)
        {
            DialogController.Instance.WriteText(onSceneStartingDialog, _player);
        }

        if (itemToCollect == InventoryController.CollectableItem.Default)
        {
            Debug.LogWarning("No item to collect has been set for " + gameObject.name);
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
        
        Dialog();
        CheckSprite();
    }

    //Checks player inventory for the required item and if the player has interacted with the character before
    private void Dialog()
    {
        if (PlayerPrefs.GetInt("Conductor") == 0)
        {
            //Displays dialog if the player has not yet interacted with the character
            WriteDialog(firstContactDialog);
            PlayerPrefs.SetInt("Conductor", 1);
        }
        else
        {
            if (itemToCollect != InventoryController.CollectableItem.Default)
            {
                if (_inventoryController.PlayerHasItem(itemToCollect))
                {
                    //Displays dialog if the player has the required item
                    WriteDialog(collectDialog);
                    if (itemToReceive != InventoryController.CollectableItem.Default)
                    {
                        //Player receives item
                        InventoryController.Instance.AddNewElementToInventory(itemToReceive);
                    }
                }
                else
                {
                    //Displays dialog if the player does not have the required item
                    WriteDialog(returningDialog);
                }
            }
            else
            {
                //Displays default returning dialog
                WriteDialog(returningDialog);
            }
        }
    }

    private void WriteDialog(String[] text)
    {
        if (DialogController.Instance.IsWriting()) return;
        DialogController.Instance.ResetIsWritingState();
        DialogController.Instance.WriteText(collectDialog, _player);
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
