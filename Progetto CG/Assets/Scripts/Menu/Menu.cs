using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// classe per gestire il main menu
public class Menu : MonoBehaviour
{
    // todo implementare controllo volume

    public static string SelectedCharacter;
    public AudioMixer audioMixer;
    
    // funzione per passare alla scena successiva
    private void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayWarrior()
    {
        SelectedCharacter = "warrior";
        PlayGame();
    }
    
    public void PlayRonin()
    {
        SelectedCharacter = "ronin";
        PlayGame();
    }
    
    public void PlayArcher()
    {
        SelectedCharacter = "archer";
        PlayGame();
    }

    // funzione per uscire dal gioco
    public void QuitGame()
    {
        Application.Quit();
    }

    // funzione per gestire il volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
