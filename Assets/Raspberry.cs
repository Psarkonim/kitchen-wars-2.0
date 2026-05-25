using UnityEngine;

namespace Assets
{
    public class Raspberry: Food
    {
        [Header("Атака")]
        [SerializeField] private GameObject simpleBullet;
        [SerializeField] private float cooldown;
        [SerializeField] private bool canAttack;
    
        private float lastAttackTime;

        protected override void Awake()
        {
            base.Awake();
            lastAttackTime = Time.time;
        }

        private void Update()
        {
            HandleAttack();
        }

        private void UpdateCanAttack()
        {
            if (Time.time >= lastAttackTime + cooldown && CheckIsEnemyInRange())
                canAttack = true;

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
            Instantiate(simpleBullet, position, Quaternion.identity);
        }
    }
}