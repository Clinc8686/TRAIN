using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        GameInputs.Instance.OnPlayerUsedLeftMouseButton += InputCation_GameInputs_OnPlayerUsedLeftMouseButton;
    }
    private void InputCation_GameInputs_OnPlayerUsedLeftMouseButton(object sender, System.EventArgs e)
    {
        Debug.Log("Left Button");
    }
}
