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
    [SerializeField] private Transform playerTargetIndicator;
    [SerializeField] private LayerMask interactableLayerMask;

    private Vector3 newPosition;
    public float speed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        newPosition = transform.position;
        playerTargetIndicator.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameInputs.Instance.OnPlayerUsedLeftMouseButton += InputAction_GameInputs_OnPlayerUsedLeftMouseButton;
        GameInputs.Instance.OnPlayerInteracted += InputAction_GameInputs_OnPlayerInteracted;
    }

    private void InputAction_GameInputs_OnPlayerInteracted(object sender, EventArgs e)
    {
        float interactableCheckRadius = 1f;
        Collider2D[] interactableColliders = Physics2D.OverlapCircleAll(
                                            transform.position, 
                                            interactableCheckRadius, 
                                            interactableLayerMask);

        interactableColliders[0].GetComponent<IInteractable>().Interact();
    }

    private void FixedUpdate()
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
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

        if (hit.collider == null) return;

        //if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
        //{
        //    interactable.Interact(this);
        //}
        //else
        if(hit.collider.TryGetComponent<Collectable>(out Collectable collectable))
        {
            if (!collectable.IsPlayerInRange()) return;

            newPosition = collectable.transform.position;
        }
        else 
        { 
            newPosition = clickPosition;
            playerTargetIndicator.position = (Vector2)clickPosition;
            playerTargetIndicator.gameObject.SetActive(true);
        }
    }
    
    private void MovePlayerToPosition()
    {
        float minTargetPosition = .5f;
        float slowDownRange = 3f;
        
        Vector3 direction = (newPosition - transform.position).normalized;
        direction.z = 0f;
        float distance = Vector3.Distance(transform.position, newPosition);

        /*if (distance > minTargetPosition)
        {
            Debug.Log("Vector not null");
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);
            
            animator.SetBool("IsWalking", true);
        }
        else
        {
            Debug.Log("player idle");
            animator.SetBool("IsWalking", false);
        }*/
        
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
