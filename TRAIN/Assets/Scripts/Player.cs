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
    private const string PLAYER_MOVE_TOP_LEFT = "TopLeft";
    private const string PLAYER_MOVE_BOTTOM_RIGHT = "BackRight";
    private const string PLAYER_MOVE_BOTTOM_LEFT = "BackLeft";

    [SerializeField] private Transform playerTargetIndicator;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Animator animator;

    private Vector3 newPosition;
    private bool _isWalking = false;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private int layerMaskWithoutPlayerTrain;
    private bool firstStep = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        newPosition = transform.position;
        playerTargetIndicator.gameObject.SetActive(false);
        
        int playerLayer = LayerMask.NameToLayer("Player");
        int trainLayer = LayerMask.NameToLayer("Train");
        layerMaskWithoutPlayerTrain = ~(1 << playerLayer | 1 << trainLayer); //Exclude player and train layer
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

        if (interactableColliders.Length == 0) return;

        interactableColliders[0].GetComponent<IInteractable>().Interact();
    }

    private void Update()
    {
        if (!_isWalking) return;

        float minDistanceToNewTargetPosition = .01f;
        if (Vector2.Distance(transform.position, newPosition) <= minDistanceToNewTargetPosition)
        {
            _isWalking = false;
            playerTargetIndicator.gameObject.SetActive(false);
        }

        PlayerAnimations();
    }
    private void FixedUpdate()
    {
        if (CheckIfPlayerCanMoveForward())
        {
            MovePlayerToPosition();
        }
    }

    private bool CheckIfPlayerCanMoveForward()
    {
        float size = 0.2f;
        if (firstStep)
        {
            size = 0.5f;
            firstStep = false;
        }
        Vector3 direction = (newPosition - transform.position).normalized;
        //Debug.DrawRay(transform.position, new Vector2(direction.x, direction.y)*2, Color.red, 2, false);
        RaycastHit2D collision = Physics2D.BoxCast(transform.position, Vector2.one, 0f, direction, 1f, layerMaskWithoutPlayerTrain);
        if (Physics2D.BoxCast(transform.position+direction, new Vector2(size, size), 0f, direction, size, layerMaskWithoutPlayerTrain))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

    
    private void InputAction_GameInputs_OnPlayerUsedLeftMouseButton(object sender, System.EventArgs e)
    {
        firstStep = true;
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
            _isWalking = true;
            newPosition = clickPosition;
            if (playerTargetIndicator != null)
            {
                playerTargetIndicator.position = (Vector2)clickPosition;
                playerTargetIndicator.gameObject.SetActive(true);
            }
        }
    }
    
    private void MovePlayerToPosition()
    {
        //float minTargetPosition = .5f;
        //float slowDownRange = 3f;
        
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

        transform.position = Vector2.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
        //if (distance >= slowDownRange)
        //{
        //    transform.position += direction * speed * Time.deltaTime;
        //} 
        //else
        //{
        //    transform.position += direction * (speed * (distance / slowDownRange)) * Time.deltaTime;
        //}
    }
    private void PlayerAnimations()
    {
        Vector2 playerPosition = transform.position;

        if(_isWalking)
        {
            if(playerPosition.x >= newPosition.x && playerPosition.y <= newPosition.y)
            {
                animator.SetBool(PLAYER_MOVE_TOP_LEFT, true);
            }
            else if(playerPosition.x >= newPosition.x && playerPosition.y >= newPosition.y)
            {
                animator.SetBool(PLAYER_MOVE_BOTTOM_RIGHT, true);
            }
            else if(playerPosition.x <= newPosition.x && playerPosition.y >= newPosition.x)
            {
                animator.SetBool(PLAYER_MOVE_BOTTOM_LEFT, true);
            }
        }
        else
        {
            animator.SetBool(PLAYER_MOVE_TOP_LEFT, false);
            animator.SetBool(PLAYER_MOVE_BOTTOM_LEFT, false);
            animator.SetBool(PLAYER_MOVE_BOTTOM_RIGHT, false);
        }
    }
}
