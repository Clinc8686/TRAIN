using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Player player;
    [SerializeField] private Transform interactionSignTransform;
    [SerializeField] private Transform interactionPointTransform;
    [SerializeField] private string dialogTextInput;
    [SerializeField] private float interactionDistance = 1f;

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
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
    public void Interact()
    {
        //float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        //if (distanceToPlayer > interactionDistance) return;

        DialogController.Instance.WriteText(dialogTextInput);
    }
}
