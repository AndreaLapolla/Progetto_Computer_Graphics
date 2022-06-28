using UnityEngine;

// classe per gestire il danno inflitto dai proiettili nemici
public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private float _lifeTime;

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        _lifeTime += Time.deltaTime;

        if (_lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void ActiavteProjectile()
    {
        _lifeTime = 0;
        gameObject.SetActive(true);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        gameObject.SetActive(false);
    }
}