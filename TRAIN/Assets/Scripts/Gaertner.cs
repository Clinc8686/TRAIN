using UnityEngine;

public class Gaertner: MonoBehaviour, IInteractable 
{
    [SerializeField] private Player player;
    [SerializeField] private Transform interactionSignTransform;
    [SerializeField] private float interactionDistance = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;
    
    public void Interact()
    {
        
        
    }
}