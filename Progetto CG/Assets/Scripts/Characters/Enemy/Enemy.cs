using UnityEngine;

// classe in cui inserire il codice comune tra ranged e melee enemy
public class Enemy : MonoBehaviour
{
    [Header("Sounds")] 
    [SerializeField] protected AudioClip[] attackSounds;
}
