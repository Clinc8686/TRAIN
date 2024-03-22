using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private SpriteRenderer _alreadySelectedSomething;
    private SpriteRenderer _renderer;
    private Collectable _collectable;
    private int _oldPrioritisation;

    private void Start()
    {
        _collectable = GetComponent<Collectable>();
        _renderer = GetComponent<SpriteRenderer>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        selectionManager.Instance.SetCurrentSelection(this);
        if (selectionManager.Instance.previouslySelectedSprite == null)
        {
            Debug.Log("null");
            selectionManager.Instance.PreviouslySelected(_renderer);
            SetColor(2, true);
        }
        else if (selectionManager.Instance.previouslySelectedSprite == _renderer)
        {
            Debug.Log("deselect");
            SetColor(0, true);
            selectionManager.Instance.PreviouslySelected(null);
        }
        else
        {
            Debug.Log("other");
            //SetColor(0, true,false);
            SetColor(2, true);
            selectionManager.Instance.PreviouslySelected(_renderer);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetColor(3, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectionManager.Instance.previouslySelectedSprite != null)
        {
            if (selectionManager.Instance.previouslySelectedSprite != _renderer)
            {
                SetColor(0, true);
            }
            else
            {
                SetColor(2, true);
            }
        }
        else
        {
            SetColor(0, true);
        }
    }

    private void Update()
    {
        if (_collectable.distanceToPlayer < _collectable.interactableRadius)
        {
            UpdatePrioritisation(1);
        }
    }

    private void SetColor(int prioritisation, bool overrider = false, bool previouslySelected = true)
    {
        UpdatePrioritisation(prioritisation, overrider);

        Debug.Log(prioritisation);
        if (!previouslySelected)
        {
            selectionManager.Instance.previouslySelectedSprite.color = Color.white;
        }
        else
        {
            _renderer.color = prioritisation switch
            {
                3 => Color.red,
                2 => Color.green,
                1 => Color.yellow,
                0 => Color.white,
                _ => throw new Exception($"Unknown prio {prioritisation}"),
            };
        }
    }

    private void UpdatePrioritisation(int prioritisation, bool overrider = false)
    {
        if (!overrider)
        {
            if (prioritisation > _oldPrioritisation)
            {
                _oldPrioritisation = prioritisation;
            }
        }
        else
        {
            _oldPrioritisation = prioritisation;
        }
    }
}