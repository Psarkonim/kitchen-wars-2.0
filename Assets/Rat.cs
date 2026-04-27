using Assets;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Rat : MonoBehaviour
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
    private List<Effect> effects;

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
    
        // Просто проверяем позицию объекта (без учета его ширины)
        if (transform.position.x > rightEdge.x + 1f)  // +1 запас
            Destroy(gameObject);
        
    }
    void HandleMove()
    {
        if (!isAttacking)
            rb.linearVelocity = new Vector2(-curSpeed, rb.linearVelocity.y);    
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void Update()
    {
        HandleMove();
        HandleAttack();
        HandleInBounds();
        foreach(var effect in effects)
            effect.ApplyEffect(this);
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
            Destroy(gameObject);
    }
}
