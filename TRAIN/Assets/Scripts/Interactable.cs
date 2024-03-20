using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string dialogTextInput;
    [SerializeField] private float collectionDistance = 1f;
   public void Interact(Player player)
   {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > collectionDistance) return;

        Debug.Log("Interacted");
        DialogController.Instance.WriteText(dialogTextInput);
   }
}
