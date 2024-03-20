using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Selection : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private SpriteRenderer _alreadySelectedSomething;
    private SpriteRenderer _renderer;
    [FormerlySerializedAs("OnClick")] [SerializeField] private UnityEvent onClick;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        selectionManager.Instance.SetCurrentSelection(this);
        if (selectionManager.Instance.previouslySelectedSprite == null)
        {
            Debug.Log("null");
            selectionManager.Instance.PreviouslySelected(_renderer);
            _renderer.color = Color.green;
        }
        else if (selectionManager.Instance.previouslySelectedSprite == _renderer)
        {
            Debug.Log("deselect");
            _renderer.color = Color.white;
            selectionManager.Instance.PreviouslySelected(null);
        }
        else
        {
            Debug.Log("other");
            selectionManager.Instance.previouslySelectedSprite.color = Color.white;
            _renderer.color = Color.green;
            selectionManager.Instance.PreviouslySelected(_renderer);
        }
        onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectionManager.Instance.previouslySelectedSprite != null)
        {
            if (selectionManager.Instance.previouslySelectedSprite != _renderer)
            {
                _renderer.color = Color.white;
            }
            else
            {
                _renderer.color = Color.green;
            }
        }
        else
        {
            _renderer.color = Color.white;
        }
    }
}