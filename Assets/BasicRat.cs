using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;

public class BasicRat : MonoBehaviour
{
    // Оставляем ОДИН Rigidbody2D для всего скрипта
    private Rigidbody2D rb;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed;
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;
    [SerializeField] private float damage;
    [SerializeField] private float curSpeed;
    [SerializeField] private float cooldown;
    [SerializeField] private bool canAttack;
    [SerializeField] private float lastAttackTime = 0f;
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool isAttacking;
    [SerializeField] private GameObject rewardFood;
    
    private List<Effect> effects;
    private bool _isKnockedBack;
    
    public float Speed => speed;
    public float CurSpeed { get => curSpeed; set => curSpeed = value; }
    public bool IsKnockedBack => _isKnockedBack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        curSpeed = speed;
        mainCamera = Camera.main;
        effects = new List<Effect>();
    }

    void Update()
    {
        // Если крыса в состоянии отскока — ПОЛНОСТЬЮ выходим из Update.
        // Она не должна ни атаковать, ни проверять границы, ни двигаться вперед.
        if (_isKnockedBack) 
        {
            return; 
        }

        HandleAttack();
        HandleInBounds();
        HandleEffect();
        HandleMove(); 
    }

    void HandleInBounds()
    {
        Vector3 rightEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, -mainCamera.transform.position.z));
    
        if (transform.position.x > rightEdge.x + 1f)
            Die();
    }

    void HandleMove()
    {
        if (!isAttacking)
            rb.linearVelocity = new Vector2(-curSpeed, rb.linearVelocity.y);    
    }

    void HandleEffect()
    {
        curSpeed = speed;

        var cellCollider = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Cell"));

        if (cellCollider != null)
        {
            var cell = cellCollider.GetComponent<Cell>();

            if (cell != null)
            {
                foreach (var effect in cell.Effects)
                {
                    if (!effects.Any(e => e.GetType() == effect.GetType()))
                        effects.Add(effect);
                    else
                    {
                        var existingEffect = loyaltyEffect(effect);
                        if (existingEffect != null) existingEffect.Refresh();
                    }
                }
            }
        }

        for (int i = effects.Count - 1; i >= 0; i--)
        {
            var effect = effects[i];
            effect.ApplyEffect(this);

            if (effect.IsEnded)
            {
                effect.RemoveEffect(this);
                effects.RemoveAt(i);
            }
        }
    }

    private Effect loyaltyEffect(Effect effect)
    {
        return effects.FirstOrDefault(e => e.GetType() == effect.GetType());
    }

    void UpdateCanAttack()
    {
        if (Time.time >= lastAttackTime + cooldown)
            canAttack = true;
    }

    void HandleAttack()
    {
        UpdateCanAttack();

        var hit = Physics2D.Raycast(transform.position, Vector2.left, range, enemyLayer);

        if (hit.collider != null)
        {
            isAttacking = true;
            curSpeed = 0;

            if (canAttack)
                Attack(hit.collider.GetComponent<Food>());
        }
        else
        {
            isAttacking = false;
            curSpeed = speed;
        }
    }

    void Attack(Food food)
    {
        if (food != null)
        {
            food.TakeDamage(damage);
            lastAttackTime = Time.time;
            canAttack = false;
        }
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;

        if (curHp <= 0) 
            Die();
    }
    
    public void TakeKnockback(Vector2 force, float duration)
    {
        if (_isKnockedBack) return;
        StartCoroutine(KnockbackRoutine(force, duration));
    }

    private IEnumerator KnockbackRoutine(Vector2 force, float duration)
    {
        _isKnockedBack = true;
        isAttacking = false;
        curSpeed = speed; // Сбрасываем скорость атаки на дефолтную

        if (rb != null)
        {
            // Принудительно «будим» физику крысы, если она уснула
            rb.WakeUp(); 
            rb.linearVelocity = force; 
        }

        yield return new WaitForSeconds(duration);

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        _isKnockedBack = false;
    }

    public void Die()
    {
        Destroy(gameObject);
        
        if (rewardFood != null)
        {
            Instantiate(rewardFood, transform.position, Quaternion.identity);
        }
    }
}