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
    private const string PLAYER_MOVE_TOP_LEFT = "BackLeft";
    private const string PLAYER_MOVE_BOTTOM_RIGHT = "TopRight";
    private const string PLAYER_MOVE_BOTTOM_LEFT = "TopLeft";
    private const string PLAYER_MOVE_TOP_RIGHT = "BackRight";

    [SerializeField] private Transform playerTargetIndicator;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private LayerMask targetLayerMask;

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
        float interactableCheckRadius = 10f;
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
        float boxCastSize = 0.2f;
        float boxCastDistance = 0.2f;
        
        if (firstStep)
        {
            boxCastDistance = 0.5f;
            firstStep = false;
        }
        Vector3 direction = (newPosition - transform.position).normalized;
        if (Physics2D.BoxCast(transform.position+direction, new Vector2(boxCastSize, boxCastSize), 0f, direction, boxCastDistance, layerMaskWithoutPlayerTrain))
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
        //if (UtilsClass.Utils.IsPointerOverUI()) return;

        firstStep = true;
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        CheckPositionIsOnBottom(mousePosition);
    }

    private void CheckPositionIsOnBottom(Vector3 position)
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(position);        
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, targetLayerMask);

        if (hit.collider == null) return;

        if (hit.collider.TryGetComponent<NotInteractable>(out NotInteractable notInteractable))
        {
            Debug.Log("Hey");
            return;
        }

        //if (hit.collider.TryGetComponent<Confiner>(out Confiner confiner)) return;

        if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
        {
            Debug.Log("Hit interactable!");
            interactable.Interact();
        }
        else if (hit.collider.TryGetComponent<Collectable>(out Collectable collectable))
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
        Vector3 direction = (newPosition - transform.position).normalized;
        direction.z = 0f;

        transform.position = Vector2.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
    }
    private void PlayerAnimations()
    {
        if(_isWalking)
        {
            Vector3 direction = (newPosition - transform.position).normalized;
            if (direction.x > 0.01f && direction.y > 0.01f)
            {
                animator.SetBool(PLAYER_MOVE_TOP_LEFT, false);
                animator.SetBool(PLAYER_MOVE_TOP_RIGHT, true);
                animator.SetBool(PLAYER_MOVE_BOTTOM_LEFT, false);
                animator.SetBool(PLAYER_MOVE_BOTTOM_RIGHT, false);
            }
            else if (direction.x < -0.01f && direction.y < -0.01f)
            {
                animator.SetBool(PLAYER_MOVE_TOP_LEFT, false);
                animator.SetBool(PLAYER_MOVE_TOP_RIGHT, false);
                animator.SetBool(PLAYER_MOVE_BOTTOM_LEFT, true);
                animator.SetBool(PLAYER_MOVE_BOTTOM_RIGHT, false);
            }
            else if (direction.x < -0.01f && direction.y > 0.01f)
            {
                animator.SetBool(PLAYER_MOVE_TOP_LEFT, true);
                animator.SetBool(PLAYER_MOVE_TOP_RIGHT, false);
                animator.SetBool(PLAYER_MOVE_BOTTOM_LEFT, false);
                animator.SetBool(PLAYER_MOVE_BOTTOM_RIGHT, false);
            }
            else if (direction.x > 0.01f && direction.y < -0.01f)
            {
                animator.SetBool(PLAYER_MOVE_TOP_LEFT, false);
                animator.SetBool(PLAYER_MOVE_TOP_RIGHT, false);
                animator.SetBool(PLAYER_MOVE_BOTTOM_LEFT, false);
                animator.SetBool(PLAYER_MOVE_BOTTOM_RIGHT, true);
            }
        }
        else
        {
            animator.SetBool(PLAYER_MOVE_TOP_LEFT, false);
            animator.SetBool(PLAYER_MOVE_TOP_RIGHT, false);
            animator.SetBool(PLAYER_MOVE_BOTTOM_LEFT, false);
            animator.SetBool(PLAYER_MOVE_BOTTOM_RIGHT, false);
        }
    }
}
