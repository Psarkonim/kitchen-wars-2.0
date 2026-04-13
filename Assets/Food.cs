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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
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

        var newBullet = Instantiate(simpleBullet, transform.position, Quaternion.identity);

    }


}
