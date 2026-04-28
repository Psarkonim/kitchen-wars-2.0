using NUnit.Framework.Constraints;
using UnityEngine;

public abstract class Food : MonoBehaviour
{
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;
    [SerializeField] protected float range = 0f;
    [SerializeField] public Sprite inventoryActiveSprite;
    [SerializeField] public Sprite inventoryPassiveSprite;

    protected Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

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

    public bool CheckIsEnemyInRange()
    {
        var isEnemyInRange = Physics2D.Raycast(transform.position, Vector2.right, range, enemyLayer);

        return isEnemyInRange.collider is not null;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}