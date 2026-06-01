using UnityEngine;

public class Chocolate : Food
{
    [Header("Chocolate Settings")]
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] public float fireRate = 1f;       
    [SerializeField] private int maxShots = 8; 

    [Header("Visual States")]
    [SerializeField] private Sprite[] chocolateStates; 
    private SpriteRenderer _spriteRenderer;
    
    [Header("Attack Settings")]
    public float attackInterval = 1.5f;
    
    [Header("Damage Settings")]
    public float bulletDamage = 5f; 
    
    private int _shotsLeft;                       
    private float _nextFireTime = 0f;                   

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _shotsLeft = maxShots; 
    }

    private void Update()
    {
        if (_shotsLeft <= 0) 
            return;

        if (Time.time >= _nextFireTime)
        {
            if (CheckIsEnemyInRange())
            {
                _nextFireTime = Time.time + fireRate; 
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        _shotsLeft--;

        if (projectilePrefab != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            
            if (bullet.TryGetComponent<Bullet>(out Bullet bulletScript))
            {
                bulletScript.SetDamage(bulletDamage);
            }
        }

        UpdateVisualState();

        if (_shotsLeft <= 0)
            Die(); 
    }

    private void UpdateVisualState()
    {
        if (chocolateStates == null || chocolateStates.Length == 0 || _spriteRenderer == null) 
            return;

        int spriteIndex = _shotsLeft - 1;

        if (spriteIndex >= 0 && spriteIndex < chocolateStates.Length)
        {
            _spriteRenderer.sprite = chocolateStates[spriteIndex];
        }
    }
}