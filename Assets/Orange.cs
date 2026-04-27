using UnityEngine;

public class Orange : Food
{
    [Header("Настройки our APPLE хахахахахаахахха")]
    [SerializeField] private float rollSpeed = 5f;
    [SerializeField] private float destroyXPosition = 15f; 
    protected override void Awake()
    {
        base.Awake();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.right, 1000, enemyLayer);
        if (hit.collider is not null)
            rb.linearVelocity = new Vector2(rollSpeed, rb.linearVelocity.y);
        if (transform.position.x >= destroyXPosition)
            Die();
        
    }
}