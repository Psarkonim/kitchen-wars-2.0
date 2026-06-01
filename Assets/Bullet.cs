using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float damage; 
    [SerializeField] float speed = 0.8f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        HandleMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BasicRat>(out BasicRat rat))
        {
            rat.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void HandleMove()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }

    // Публичный метод-сеттер, который мы вызываем при спавне пули в Chocolate и Raspberry
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }
}