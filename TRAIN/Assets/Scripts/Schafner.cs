using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schafner : MonoBehaviour, IInteractable
{
    [SerializeField] private Player player;
    [SerializeField] private Transform interactionSignTransform;
    [SerializeField] private string dialogTextInput;
    [SerializeField] private float interactionDistance = 1f;
    [SerializeField] private CollectableSO collectableSO;
    [SerializeField] private string[] text;
    [SerializeField] private string[] textAfterItem;
    [SerializeField] private string[] textwithBubatz;
    private bool firstTimeDialog = false;
    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= interactionDistance)
        {
            interactionSignTransform.gameObject.SetActive(true);
        }
        else
        {
            interactionSignTransform.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > interactionDistance) return;
        
        if (InventoryController.Instance._hasBubads)
        {
            if (DialogController.Instance.IsWriting()) return;
            DialogController.Instance.ResetIsWritingState();
            DialogController.Instance.WriteText(textwithBubatz, player);
            InventoryController.Instance.AddNewElementToInventory(collectableSO);
        }
        else
        {
            if (firstTimeDialog)
            {
                if (DialogController.Instance.IsWriting()) return;
                DialogController.Instance.ResetIsWritingState();
                DialogController.Instance.WriteText(text, player);
            }
            else
            {
                if (DialogController.Instance.IsWriting()) return;
                DialogController.Instance.ResetIsWritingState();
                DialogController.Instance.WriteText(textAfterItem, player);
            } 
        }
    }
}
