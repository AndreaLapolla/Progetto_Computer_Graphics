using UnityEngine;

// classe per controllare il movimento della telecamera
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject characterSelector;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;

    private float lookAhead;
    private Transform player;

    // bisogan stabilire all'inizio chi seguire
    private void Awake()
    {
        player = characterSelector.GetComponent<CharacterSelector>().GetPlayedCharacter().transform;
    }

    private void Update()
    {
        // ad ogni frame la telecamera si sposterà con il personaggio
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), 
            Time.deltaTime * cameraSpeed);
    }
}
