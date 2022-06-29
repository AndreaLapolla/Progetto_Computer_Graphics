using UnityEngine;

// classe per controllare il movimento della telecamera
public class CameraController : MonoBehaviour
{
    [Header("Character Selector")]
    [SerializeField] private GameObject characterSelector;
    
    [Header("Camera Parameters")]
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;

    private float _lookAhead;
    private Transform _player;

    // bisogan stabilire all'inizio chi seguire
    private void Awake()
    {
        _player = characterSelector.GetComponent<CharacterSelector>().GetPlayedCharacter().transform;
    }

    private void Update()
    {
        // ad ogni frame la telecamera si sposterà con il personaggio
        transform.position = new Vector3(_player.position.x, transform.position.y, transform.position.z);
        _lookAhead = Mathf.Lerp(_lookAhead, (aheadDistance * _player.localScale.x), 
            Time.deltaTime * cameraSpeed);
    }
}
