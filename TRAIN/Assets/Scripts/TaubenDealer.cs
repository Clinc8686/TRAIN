using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaubenDealer : MonoBehaviour, IInteractable
{
    [SerializeField] private Player player;
    [SerializeField] private Transform interactionSignTransform;
    [SerializeField] private string dialogTextInput;
    [SerializeField] private float interactionDistance = 1f;
    private void Update()
    {
        if (!InventoryController.Instance.HasSamen()) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= interactionDistance)
        {
            interactionSignTransform.gameObject.SetActive(true);
        }
        else
        {
            if (interactionSignTransform.gameObject.activeInHierarchy)
                interactionSignTransform.gameObject.SetActive(false);
        }
    }
    public void Interact()
    {
        if (!InventoryController.Instance.HasSamen()) return;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > interactionDistance) return;

        DialogController.Instance.WriteText(dialogTextInput);
    }
}
