using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class selectionManager : MonoBehaviour
{
    public static selectionManager Instance { get; private set; }
    private Selection _currentSelection;
    public SpriteRenderer previouslySelectedSprite;

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
        _currentSelection = selection;
    }

    public void PreviouslySelected(SpriteRenderer newSelection)
    {
        previouslySelectedSprite = newSelection;
    }
}
