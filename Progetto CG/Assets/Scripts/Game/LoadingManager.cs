using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// classe serve per passare da una scena all'altra, gestendo in punto di arrivo
public class LoadingManager : MonoBehaviour
{
    [Header("Sounds")] 
    [SerializeField] private AudioClip levelEndSound;
    
    private const int MaxSceneIndex = 3;

    // al raggiungimento del traguaro si passerà al prossimo livello, o al menu iniziale
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            SoundManager.Instance.PlaySound(levelEndSound);
            StartCoroutine(EndLevel(1));
            MessageText.Instance.WriteMessage("Livello completato");
        }
    }

    private IEnumerator EndLevel(float waitTime)
    {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(nextSceneIndex <= MaxSceneIndex ? nextSceneIndex : 0);
    }
}
