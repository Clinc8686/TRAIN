using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    private bool trainIsMoving = false;
    private bool coroutineFinished = false;
    private int defaultLayer;
    private int trainLayer;
    private void Awake()
    {
        defaultLayer = LayerMask.NameToLayer("Default");
        trainLayer = LayerMask.NameToLayer("Train");
    }

    private void FixedUpdate()
    {
        CheckIfTrainIsMoving();
        if (!trainIsMoving)
        {
            SetGameLayerRecursive(transform.parent.transform.parent.gameObject, defaultLayer);
        }
        else
        {
            SetGameLayerRecursive(transform.parent.transform.parent.gameObject, trainLayer);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Player>(out Player player) && player.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
        {
            if (!trainIsMoving)
            {
                spriteRenderer.enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Player>(out Player player) && player.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
        {
            if (!trainIsMoving)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                //TODO: player drives to next station
            }
        }
    }
    
    Vector2 oldPos;
    private void CheckIfTrainIsMoving()
    {
        Vector2 newPos = transform.parent.transform.parent.position;
        newPos = new Vector2((float)System.Math.Round(newPos.x, 2), (float)System.Math.Round(newPos.y, 2));
        
        if (oldPos.x != newPos.x && oldPos.y != newPos.y)
        {
            trainIsMoving = true;
        }
        else
        {
            trainIsMoving = false;
        }

        oldPos = newPos;
    }
    
    private void SetGameLayerRecursive(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform child in gameObject.transform)
        {
            SetGameLayerRecursive(child.gameObject, layer);
        }
    }

}
