using UnityEngine;
using UnityEngine.UI;
public class HomeMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Button playGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Transform sceneCloseTransitionTransform;
    private void Awake()
    {
        sceneCloseTransitionTransform.gameObject.SetActive(false);

        playGameButton.onClick.AddListener(() =>
        {
            sceneCloseTransitionTransform.gameObject.SetActive(true);
        });

        optionsButton.onClick.AddListener(() =>
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        });
        
        backButton.onClick.AddListener(() =>
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        });
        
        quitGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
