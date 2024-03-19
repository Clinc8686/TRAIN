using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector3 newPosition;
    public float speed = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
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
        CheckPositionIsOnBottom(Mouse.current.position.ReadValue());
    }

    private void CheckPositionIsOnBottom(Vector3 position)
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(position);
        
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.down, 10f);
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
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
