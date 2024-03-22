using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public static StationManager Instance { get; private set; }
    
    private List<GameObject> trains = new List<GameObject>();
    private int nextTrain = 0;
    private float timer = 10f;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        GetAllTrains();
        DisableAllTrains();
    }

    private void GetAllTrains()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Train"))
            {
                trains.Add(transform.GetChild(i).gameObject);
            }
        }
    }
    
    void Update()
    {
        //Important: Train animation lenght must be 10 seconds
        timer += Time.deltaTime;
        if (timer > 10)
        {
            MoveNextTrain();
            timer = 0f;
        }
    }

    public void OnCallTrainCLicked(int trainNumber)
    {
        nextTrain = trainNumber;
    }

    private void MoveNextTrain()
    {
        DisableAllTrains();
        if (nextTrain >= trains.Count)
        {
            nextTrain = 0;
        }
        trains[nextTrain].SetActive(true);
        nextTrain++;
    }

    private void DisableAllTrains()
    {
        foreach (GameObject train in trains)
        {
            train.SetActive(false);
            train.TryGetComponent(out Animator animator);
            animator.Update(0f);
        }
    }
}
