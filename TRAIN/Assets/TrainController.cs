using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    private bool trainIsMoving = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.TryGetComponent<Player>(out Player player) && player.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
        {
            StartCoroutine(CheckIfTrainIsMoving());
            if (!trainIsMoving)
            {
                spriteRenderer.enabled = false;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Player>(out Player player) && player.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
        {
            if (!trainIsMoving)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                //TODO: player drives in train to next station
            }
        }
    }

    IEnumerator CheckIfTrainIsMoving()
    {
        Vector2 oldPos = transform.position;
        yield return new WaitForSeconds(0.02f);
        Vector2 newPos = transform.position;
        if (oldPos != newPos)
        {
            trainIsMoving = true;
        }
        else
        {
            trainIsMoving = false;
        }
    }
}
