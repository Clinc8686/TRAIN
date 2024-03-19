using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector3 newPosition;
    public float speed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameInputs.Instance.OnPlayerUsedLeftMouseButton += InputAction_GameInputs_OnPlayerUsedLeftMouseButton;
    }

    private void Update()
    {
        MovePlayerToPosition();
    }

    private void InputAction_GameInputs_OnPlayerUsedLeftMouseButton(object sender, System.EventArgs e)
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        CheckPositionIsOnBottom(mousePosition);
    }

    private void CheckPositionIsOnBottom(Vector3 position)
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(position);
        
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.down, 10f);
        if (hit.collider == null)
        {
            return;
        }
        if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
        else 
        { 
            newPosition = clickPosition;
        }
    }
    
    private void MovePlayerToPosition()
    {
        Vector3 direction = (newPosition - transform.position).normalized;

        if (direction.x > 0.05f)
        {
            _spriteRenderer.sprite = null;
        } else if (direction.x < -0.05f)
        {
            _spriteRenderer.sprite = null;
        } else if (direction.y > 0.05f)
        {
            _spriteRenderer.sprite = null;
        } else if (direction.y < -0.05f)
        {
            _spriteRenderer.sprite = null;
        }

        float minTargetPosition = .5f;
        float slowDownRange = 3f;
        float distance = Vector3.Distance(transform.position, newPosition);
        if (distance >= slowDownRange)
        {
            transform.position += direction * speed * Time.deltaTime;
        } 
        else
        {
            transform.position += direction * (speed * (distance / slowDownRange)) * Time.deltaTime;
        }
    }
}
