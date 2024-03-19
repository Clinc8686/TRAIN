using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuController : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Transform sceneCloseTransitionTransform;
    private void Awake()
    {
        sceneCloseTransitionTransform.gameObject.SetActive(false);

        startGameButton.onClick.AddListener(() =>
        {
            sceneCloseTransitionTransform.gameObject.SetActive(true);
        });

        quitGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
