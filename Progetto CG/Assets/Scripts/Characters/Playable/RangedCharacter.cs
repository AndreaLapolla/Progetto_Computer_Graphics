using UnityEngine;

// classe specializzata per i personaggi che infliggono danno a distanza
public class RangedCharacter : Character
{
    [Header("Ranged Attack Parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    
    // lanciata durante l'animazione
    private void FireArrow()
    {
        arrows[0].transform.position = firePoint.position;
        arrows[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x), damage);
    }
}
