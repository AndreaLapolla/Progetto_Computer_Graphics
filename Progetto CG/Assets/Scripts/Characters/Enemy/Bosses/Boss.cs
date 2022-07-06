using System;
using UnityEngine;

// classe per gestire i comportamenti fondamentali di un boss
public class Boss : MonoBehaviour
{
    [Header("Character Selector")]
    [SerializeField] private GameObject characterSelector;
    
    [Header("Flip")]
    [SerializeField] private bool isFlipped = false;
    
    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = characterSelector.GetComponent<CharacterSelector>().GetPlayedCharacter().transform;
    }

    // funzione per direzionarsi sempre verso il player
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > _playerTransform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < _playerTransform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
