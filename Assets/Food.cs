using UnityEngine;

public abstract class Food : MonoBehaviour
{
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;

    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    

    public float CurHp => curHp;
    public LayerMask enemyLayer;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        curHp = maxHp;
    }

    public virtual void TakeDamage(float damage)
    {
        curHp -= damage;
        
        if (curHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}