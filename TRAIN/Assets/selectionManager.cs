using UnityEngine;

public class selectionManager : MonoBehaviour
{
    public static selectionManager Instance { get; private set; }
    public Selection _currentSelection { get; private set; }
    public Selection PreviouslySelected { get; private set; }
    public Selection CurrentHover { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public void SetCurrentSelection(Selection selection)
    {
        SetPreviouslySelected(_currentSelection);
        _currentSelection = selection;
    }

    
    private void SetPreviouslySelected(Selection newSelection)
    {
        PreviouslySelected = newSelection;
    }
}
