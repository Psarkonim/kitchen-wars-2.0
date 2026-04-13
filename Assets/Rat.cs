using Assets;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Rat : MonoBehaviour
{
    Rigidbody2D rb;

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
    private List<Effect> effects;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        curSpeed = speed;
        effects = new List<Effect>();
    }
    void Start()
    {
        Debug.Log("Пуск");
    }
    
    void HandleMove()
    {
        rb.linearVelocity = new Vector2(-curSpeed, rb.linearVelocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        HandleMove();
        HandleAttack();
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

        if (!canAttack)
            return;

        canAttack = false;

        var hit = Physics2D.Raycast(transform.position, Vector2.right, range, enemyLayer);

        if (hit.collider)
            Attack(hit.collider.GetComponent<Food>());
    }
    void Attack(Food food)
    {
        food.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;

        if (curHp <= 0)
            Destroy(gameObject);
    }
}
