using UnityEngine;
using UnityEngine.SceneManagement;

// classe per gestire il menu di pausa del gioco
public class PauseMenu : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject pauseMenuUI;
    
    private static bool _gameIsPaused;

    private void Update()
    {
        // esc è il tasto per la pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
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
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    // funzione per chiudere il menu di pause e ripristinare lo scorrere del tempo
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    // funzione per tornare al menu principale
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
