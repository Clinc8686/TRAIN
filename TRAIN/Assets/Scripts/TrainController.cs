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
    private bool trainContainsPlayer = false;
    [SerializeField] private bool LastTrain;
    
    public SceneLoader.Scenes nextScene;
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
            if (LastTrain)
            {
                if (InventoryController.Instance._hasTicket && InventoryController.Instance._hasTrain)
                {
                    SetGameLayerRecursive(transform.parent.transform.parent.gameObject, defaultLayer);
                }
                else
                {
                    SetGameLayerRecursive(transform.parent.transform.parent.gameObject, trainLayer);
                }
            }
            else
            {
                SetGameLayerRecursive(transform.parent.transform.parent.gameObject, defaultLayer);
            }
        }
        else
        {
            SetGameLayerRecursive(transform.parent.transform.parent.gameObject, trainLayer);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Player>(out Player player) && player.transform.GetChild(0))
        {
            if (!trainIsMoving)
            {
                if (LastTrain && InventoryController.Instance.HasAllInventoryElements())
                {
                    if (InventoryController.Instance.HasAllInventoryElements())
                    {
                        player.transform.GetChild(0).gameObject.SetActive(false);
                        trainContainsPlayer = true;
                    }
                }
                else
                {
                    player.transform.GetChild(0).gameObject.SetActive(false);
                    trainContainsPlayer = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Player>(out Player player) && player.transform.GetChild(0))
        {
            if (!trainIsMoving)
            {
                player.transform.GetChild(0).gameObject.SetActive(true);
                trainContainsPlayer = false;
            }
            else
            {
                if (trainContainsPlayer)
                {
                    SceneLoader.Load(nextScene);
                }
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
