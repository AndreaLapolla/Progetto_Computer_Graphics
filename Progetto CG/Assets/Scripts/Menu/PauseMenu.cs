using UnityEngine;
using UnityEngine.SceneManagement;

// classe per gestire il menu di pausa del gioco
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Update()
    {
        // esc è il tasto per la pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                // riprendi
                Resume();
            }
            else
            {
                // pausa
                Pause();
            }
        }
    }

    // funzione per far apparire il menu e fermare il tempo
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // funzione per chiudere il menu di pause e ripristinare lo scorrere del tempo
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // funzione per tornare al menu principale
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
