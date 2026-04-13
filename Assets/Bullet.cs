using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] float speed = 0.8f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rat>(out Rat rat))
        {
            rat.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void HandleMove()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }
}
