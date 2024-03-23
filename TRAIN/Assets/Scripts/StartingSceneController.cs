using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSceneController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private string[] text;

    private int _index = 0;
    private void Start()
    {
        //DialogController.Instance.WriteText(text, player);
        //player.enabled = true;
        GameInputs.Instance.OnPlayerTextSkipped += PlayerInputSyste_GameInputs_OnPlayerTextSkipped;

        player.enabled = false;
        DialogController.Instance.WriteText(text[_index++]);
    }
    private void Update()
    {
        if(_index >= text.Length - 1)
        {
            GameInputs.Instance.OnPlayerTextSkipped -= PlayerInputSyste_GameInputs_OnPlayerTextSkipped;
            DialogController.Instance.ResetWritingStateAndDeactivateUI();
            player.enabled = true;
            Destroy(this);
        }
    }
    private void PlayerInputSyste_GameInputs_OnPlayerTextSkipped(object sender, System.EventArgs e)
    {
        if(_index >= text.Length - 1)
        {
            GameInputs.Instance.OnPlayerTextSkipped -= PlayerInputSyste_GameInputs_OnPlayerTextSkipped;
        }

        DialogController.Instance.ResetIsWritingState();
        DialogController.Instance.WriteText(text[_index++]);
    }
}
