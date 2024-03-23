using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaubenDealer : MonoBehaviour, IInteractable
{
    [SerializeField] private Player player;
    [SerializeField] private Transform interactionSignTransform;
    //[SerializeField] private string dialogTextInputWithSamen;
    //[SerializeField] private string dialogTextInputWithOutSamen;
    [SerializeField] private float interactionDistance = 1f;
    [SerializeField] private CollectableSO collectableSO;
    [SerializeField] private string[] textWithOutSamen;
    [SerializeField] private string[] textWithSamen;

    private int _index = 0;
    private bool _hasFinishedJob;
    private string[] text;
    private void Start()
    {
        GameInputs.Instance.OnPlayerTextSkipped += PlayerInputSystem_GameInputs_OnPlayerTextSkipped;
    }
    private void PlayerInputSystem_GameInputs_OnPlayerTextSkipped(object sender, System.EventArgs e)
    {
        if (_hasFinishedJob) return;

        if (_index >= text.Length - 1)
        {
            ResetState();
        }

        DialogController.Instance.ResetIsWritingState();
        DialogController.Instance.WriteText(text[_index++]);
    }
    private void Update()
    {
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

        if (DialogController.Instance.IsWriting()) return;

        if (_index >= text.Length - 1)
        {
            ResetState();
        }

        DialogController.Instance.WriteText(text[_index++]);
    }
    public void Interact()
    {
        _hasFinishedJob = false;
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > interactionDistance) return;

        if (!InventoryController.Instance.HasSamen())
        {
            //DialogController.Instance.WriteText(dialogTextInputWithOutSamen);
            text = textWithOutSamen;
        }
        else
        {
            text = textWithSamen;
            //DialogController.Instance.WriteText(dialogTextInputWithSamen);
            InventoryController.Instance.AddNewElementToInventory(collectableSO);
        }
    }
    private void ResetState()
    {
        DialogController.Instance.ResetWritingStateAndDeactivateUI();
        _hasFinishedJob = true;
    }
}
