using UnityEngine;

// classe per controllare il movimento della telecamera
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject warrior;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject ronin;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;

    private float lookAhead;
    private Transform player;

    private void Awake()
    {
        SetFollowedCharacter();
    }

    private void Update()
    {
        // ad ogni frame la telecamera si sposterà con il personaggio
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), 
            Time.deltaTime * cameraSpeed);
    }

    // sceglie quale personaggio seguire e disattiva gli altri
    private void SetFollowedCharacter()
    {
        if (warrior.activeSelf)
        {
            player = warrior.transform;
            archer.SetActive(false);
            ronin.SetActive(false);
        }
        else if (ronin.activeSelf)
        {
            player = ronin.transform;
            warrior.SetActive(false);
            archer.SetActive(false);
        }
        else if (archer.activeSelf)
        {
            player = archer.transform;
            warrior.SetActive(false);
            ronin.SetActive(false);
        }
    }
}
