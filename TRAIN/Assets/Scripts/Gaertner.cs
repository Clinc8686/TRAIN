using UnityEngine;

public class Gaertner: MonoBehaviour, IInteractable 
{
    [SerializeField] private Player player;
    [SerializeField] private Transform interactionSignTransform;
    [SerializeField] private float interactionDistance = 1f;
    
    [SerializeField] private string[] text;
    [SerializeField] private string[] textAfterItem;
    
    private int _hasFinishedJob;
    private bool _jobIsDone = false;
    private int _index = 0;

    private void Update()
    {
        if (_jobIsDone) return;

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
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer > interactionDistance) return;

            if (InventoryController.Instance._hasSun)
            {
                if (DialogController.Instance.IsWriting()) return;
                DialogController.Instance.ResetIsWritingState();
                DialogController.Instance.WriteText(textAfterItem, player);
                _jobIsDone = true;
            }
            else
            {
                if (DialogController.Instance.IsWriting()) return;
                DialogController.Instance.ResetIsWritingState();
                DialogController.Instance.WriteText(text, player);
                _jobIsDone = false;
            }
        }
}