using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vogelscheuche : MonoBehaviour, IInteractable
{
    [SerializeField] private Player player;
    [SerializeField] private Transform interactionSignTransform;
    [SerializeField] private float interactionDistance = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;
    [SerializeField] private CollectableSO collectableSO;

    private int _hasFinishedJob;
    private bool _jobIsDone = false;
    private void Awake()
    {
        _hasFinishedJob = PlayerPrefs.GetInt("Vogelscheuche");

        if(_hasFinishedJob == 1)
        {
            _jobIsDone = true;
            spriteRenderer.sprite = sprite;
        }
    }
    private void Update()
    {
        //if (_jobIsDone) return;

        if (!InventoryController.Instance.HasSun()) return;


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
        //if (_jobIsDone) return;

        if (!InventoryController.Instance.HasSun()) return;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > interactionDistance) return;

        spriteRenderer.sprite = sprite;
        PlayerPrefs.SetInt("Vogelscheuche", 0);
        _jobIsDone = true;
        InventoryController.Instance.AddNewElementToInventory(collectableSO);
    }
}
