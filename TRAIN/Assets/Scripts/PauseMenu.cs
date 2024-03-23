using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    public GameObject pauseMenuCanvas;
    public bool isPaused;

    public bool isOptions;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (isOptions)
        {
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(true);
            isOptions = false;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        isOptions = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeMenuScene");
        isPaused = false;
    }
}