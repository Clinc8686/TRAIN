using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private SpriteRenderer _alreadySelectedSomething;
    private SpriteRenderer _renderer;
    private Collectable _collectable;
    public const int RED = 3;
    public const int GREEN = 2;
    public const int YELLOW = 1;
    public const int WHITE = 0;

    void Start()
    {
        _collectable = GetComponent<Collectable>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectionManager.Instance._currentSelection == this)
        {
            SetColor(WHITE);
            selectionManager.Instance.SetCurrentSelection(null);
        }
        else
        {
            SetColor(GREEN);
            selectionManager.Instance.SetCurrentSelection(this);
            if (selectionManager.Instance.PreviouslySelected != null)
            {
                selectionManager.Instance.PreviouslySelected.SetColor(WHITE);
            }
        }
        if (selectionManager.Instance.CurrentHover == this)
        {
            SetColor(RED);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectionManager.Instance.CurrentHover = this;
        SetColor(RED);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectionManager.Instance.CurrentHover = null;
        if (selectionManager.Instance._currentSelection == this)
        {
            SetColor(GREEN);
        }
        else
        {
            SetColor(IsInRange() ? YELLOW : WHITE);
        }
    }

    private bool IsInRange()
    {
        return _collectable.distanceToPlayer < _collectable.interactableRadius;
    }

    private void Update()
    {
        bool isCurrentlySelected = selectionManager.Instance._currentSelection &&
                                   selectionManager.Instance._currentSelection == this;
        
        bool isCurrentlyHovering = selectionManager.Instance.CurrentHover &&
                                      selectionManager.Instance.CurrentHover == this;
        //ändere nur RangeFarbe wenn nicht ausgewählt und nicht gehovered
        if (!isCurrentlySelected && !isCurrentlyHovering && IsInRange())
        {
            SetColor(YELLOW);
        }
    }

    private void SetColor(int prioritisation)
    {
        _renderer.color = prioritisation switch
        {
            RED => Color.red,
            GREEN => Color.green,
            YELLOW => Color.yellow,
            WHITE => Color.white,
            _ => throw new Exception($"Unknown prio {prioritisation}"),
        };
    }
}