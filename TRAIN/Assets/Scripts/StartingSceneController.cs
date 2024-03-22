using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSceneController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private string[] text;

    private int index = 0;
    private void Start()
    {
        DialogController.Instance.WriteText(text, player);
        //player.enabled = true;
        //GameInputs.Instance.OnPlayerTextSkipped += PlayerInputSyste_GameInputs_OnPlayerTextSkipped;

        player.enabled = false;
        //DialogController.Instance.WriteText(text[index++]);
    }
    //private void PlayerInputSyste_GameInputs_OnPlayerTextSkipped(object sender, System.EventArgs e)
    //{
    //    DialogController.Instance.ResetIsWritingState();
    //    DialogController.Instance.WriteText(text[index++]);
    //}
}
