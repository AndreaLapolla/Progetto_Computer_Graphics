using UnityEngine;
using UnityEngine.SceneManagement;

// classe serve per passare da una scena all'altra
public class LoadingManager : MonoBehaviour
{
    // todo migliarare il codice per permettere la navigazione
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //SceneManager.LoadScene(1);
        }
    }
}
