using UnityEngine;
using UnityEngine.SceneManagement;

// classe per gestire il main menu
public class MainMenu : MonoBehaviour
{
    // todo aggiungere un menu dei livelli e dei personaggi
    public void playGame()
    {
        // per passare alla scena successiva
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // funzione per uscire dal gioco
    public void quitGame()
    {
        Application.Quit();
    }
}
