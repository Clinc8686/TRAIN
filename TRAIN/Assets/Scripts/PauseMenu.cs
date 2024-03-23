using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    public GameObject pauseMenuCanvas;
    public static bool IsPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
    
    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    
    public void backButton()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeMenuScene");
        IsPaused = false;
    }
}
