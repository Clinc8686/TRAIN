using UnityEngine;

public class Gaertner: MonoBehaviour, IInteractable 
{
    [SerializeField] private Player player;
    [SerializeField] private Transform interactionSignTransform;
    [SerializeField] private float interactionDistance = 1f;
    
    [SerializeField] private string[] text;
    [SerializeField] private Transform skipTextTransform;
    
    private int _hasFinishedJob;
    private bool _jobIsDone = false;
    private int _index = 0;
    private void Awake()
    {
        _hasFinishedJob = PlayerPrefs.GetInt("Gaertner");
        if(_hasFinishedJob == 1)
        {
            _jobIsDone = true;
        }
    }
    
    private void Start()
    {
        GameInputs.Instance.OnPlayerTextSkipped += PlayerInputSystem_GameInputs_OnPlayerTextSkipped;
        player.enabled = false;
    }
    
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
        
        if (DialogController.Instance.IsWriting()) return;

        if(_index >= text.Length - 1)
        {
            ResetState();
        }
    }
    
    private void PlayerInputSystem_GameInputs_OnPlayerTextSkipped(object sender, System.EventArgs e)
    {
        if (_jobIsDone) return;

        if (_index >= text.Length - 1)
        {
            ResetState();
        }

        DialogController.Instance.ResetIsWritingState();
        DialogController.Instance.WriteText(text[_index++]);
    }
    
    public void Interact()
    {   
        if (_jobIsDone) return;
        
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > interactionDistance) return;

        if (InventoryController.Instance.HasSun())
        {
            PlayerPrefs.SetInt("Gaertner", 1);
            _jobIsDone = true;
        }
        else
        {
            Debug.Log(DialogController.Instance.IsWriting());
            if (DialogController.Instance.IsWriting()) return;
            DialogController.Instance.ResetIsWritingState();
            DialogController.Instance.WriteText(text, player);
            PlayerPrefs.SetInt("Gaertner", 0);
            _jobIsDone = false;
        }
    }

    
    private void ResetState()
    {
        if(skipTextTransform != null) Destroy(skipTextTransform.gameObject);
        DialogController.Instance.ResetWritingStateAndDeactivateUI();
        player.enabled = true;
        _jobIsDone = true;
        //PlayerPrefs.SetInt("Intro Scene", 1);
    }
}