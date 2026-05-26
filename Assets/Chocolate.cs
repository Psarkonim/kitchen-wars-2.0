using UnityEngine;

public class Chocolate : Food
{
    [Header("Chocolate Settings")]
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private float fireRate = 0.5f;        
    
    private int _shotsLeft = 8;                       
    private float _nextFireTime = 0f;                   

    private void Update()
    {
        if (_shotsLeft <= 0) 
            return;

        if (Time.time >= _nextFireTime)
        {
            if (CheckIsEnemyInRange())
            {
                Shoot();
                _nextFireTime = Time.time + fireRate; 
            }
        }
    }

    private void Shoot()
    {
        _shotsLeft--;

        Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        if (_shotsLeft <= 0)
            Die(); 
    }
}