using UnityEngine;

// classe per controllare il movimento della telecamera
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;

    private float lookAhead;

    private void Update()
    {
        // ad ogni frame la telecamera si sposterà con il personaggio
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), 
            Time.deltaTime * cameraSpeed);
    }
}
