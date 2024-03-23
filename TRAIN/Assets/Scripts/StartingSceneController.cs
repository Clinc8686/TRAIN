using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSceneController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private string[] text;
    [SerializeField] private Transform skipTextTransform;

    private int _index = 0;
    private bool _hasFinishedJob;
    //private int _wasAlreadyOnIntroScene;
    private void Start()
    {
        Debug.Log("Aktuell " + text[_index]);

        //_wasAlreadyOnIntroScene = PlayerPrefs.GetInt("Intro Scene");

        //if(_wasAlreadyOnIntroScene == 1)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        GameInputs.Instance.OnPlayerTextSkipped += PlayerInputSystem_GameInputs_OnPlayerTextSkipped;
        player.enabled = false;
    }
    private void Update()
    {
        if (_hasFinishedJob) return;

        if (DialogController.Instance.IsWriting()) return;

        if(_index >= text.Length - 1)
        {
            ResetState();
        }

        DialogController.Instance.WriteText(text[_index++]);
    }
    private void PlayerInputSystem_GameInputs_OnPlayerTextSkipped(object sender, System.EventArgs e)
    {
        if (_hasFinishedJob) return;

        if (_index >= text.Length - 1)
        {
            ResetState();
        }

        DialogController.Instance.ResetIsWritingState();
        DialogController.Instance.WriteText(text[_index++]);
    }
    private void ResetState()
    {
        if(skipTextTransform != null) Destroy(skipTextTransform.gameObject);
        DialogController.Instance.ResetWritingStateAndDeactivateUI();
        player.enabled = true;
        _hasFinishedJob = true;
        //PlayerPrefs.SetInt("Intro Scene", 1);
    }
}
