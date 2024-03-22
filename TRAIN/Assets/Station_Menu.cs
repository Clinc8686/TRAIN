using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_Menu : MonoBehaviour
{
    [SerializeField] private GameObject stationMenu;
    
    public void OnOpenStationMenuClicked()
    {
        stationMenu.SetActive(true);
    }

    public void OnCloseStationMenuClicked()
    {
        stationMenu.SetActive(false);
    }
    
    public void OnCallTrainClicked(int trainNumber)
    {
        StationManager.Instance.OnCallTrainCLicked(trainNumber);
    }
}
