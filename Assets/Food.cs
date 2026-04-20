using Assets;
using UnityEngine;
using UnityEngine.Rendering;

public class Food : MonoBehaviour
{
    [SerializeField] private GameObject simpleBullet;
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;
    [SerializeField] private float cooldown;
    [SerializeField] private bool canAttack;
    [SerializeField] private float lastAttackTime;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        HandleAttack();
    }

    void UpdateCanAttack()
    {
        if (Time.time >= lastAttackTime + cooldown)
            canAttack = true;
    } 

    public void TakeDamage(float damage)
    {
        curHp -= damage;

        if (curHp <= 0)
            Destroy(gameObject);
    }

    private void HandleAttack()
    {
        UpdateCanAttack();

        if (!canAttack)
            return;

        canAttack = false;
        lastAttackTime = Time.time;
        var position = transform.position;
        position.x += spriteRenderer.bounds.extents.x + 0.1f;
        var newBullet = Instantiate(simpleBullet, position, Quaternion.identity);

    }
}
