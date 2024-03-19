using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string dialogTextInput;
   public void Interact()
   {
        Debug.Log("Interacted");
        DialogController.Instance.WriteText(dialogTextInput);
   }
}
