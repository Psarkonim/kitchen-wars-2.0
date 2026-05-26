using Assets;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class BasicRat : MonoBehaviour
{
    Rigidbody2D rb;
    
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

    public float Speed => speed;
    public float CurSpeed { get => curSpeed; set => curSpeed = value; }
    public bool IsKnockedBack { get; set; } = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        curSpeed = speed;
        mainCamera = Camera.main;
        effects = new List<Effect>();
    }

    void Start()
    {
    }

    void HandleInBounds()
    {
        Vector3 rightEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, -mainCamera.transform.position.z));
    
        if (transform.position.x > rightEdge.x + 1f)
            Die();
        
    }
    void HandleMove()
    {
        if (IsKnockedBack) 
            return;
        if (!isAttacking)
            rb.linearVelocity = new Vector2(-curSpeed, rb.linearVelocity.y);    
    }

    void HandleEffect()
    {
        if (!IsKnockedBack)
        {
            curSpeed = speed;
        }

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
                        var existingEffect = effects.FirstOrDefault(e =>  e.GetType() == effect.GetType());
                        existingEffect.Refresh();
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

    void Update()
    {
        HandleAttack();
        HandleInBounds();
        HandleEffect();
        HandleMove(); 
        
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
            if (IsKnockedBack)
            {
                isAttacking = false;
                return;
            }
            
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

    public void Die()
    {
        Destroy(gameObject);
        
        if (rewardFood != null)
        {
            Instantiate(rewardFood, transform.position, Quaternion.identity);
        }
    }
}
