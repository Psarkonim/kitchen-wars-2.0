using UnityEngine;

namespace Assets
{
    public class Raspberry : Food
    {
        [Header("Атака")]
        [SerializeField] protected GameObject simpleBullet;
        [SerializeField] public float cooldown = 2f; 
        [SerializeField] protected bool canAttack;
        
        [Header("Damage Settings")]
        public float bulletDamage = 4f; 
        
        [Header("Визуал Выстрела (Базовый)")]
        [SerializeField] protected Sprite shootSprite;        // Спрайт открытого рта
        [SerializeField] protected float changeDuration = 0.15f; // На сколько открывается рот
        
        protected Sprite defaultSprite;   // Обычный спрайт
        private float attackTimer = 0f;    // Таймер кулдауна
        private float mouthTimer = 0f;     // Таймер для закрытия рта

        protected override void Awake()
        {
            base.Awake();
    
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
                defaultSprite = spriteRenderer.sprite;
        }


        private void Update()
        {
            // 1. Считаем таймер перезарядки
            if (attackTimer > 0)
                attackTimer -= Time.deltaTime;

            // 2. Считаем таймер рта: если время вышло — закрываем рот
            if (mouthTimer > 0)
            {
                mouthTimer -= Time.deltaTime;
                if (mouthTimer <= 0 && spriteRenderer != null && defaultSprite != null)
                {
                    spriteRenderer.sprite = defaultSprite; // Закрыли рот
                }
            }

            HandleAttack();
        }

        private void UpdateCanAttack()
        {
            // Если таймер ещё тикает, атаковать нельзя
            if (attackTimer > 0f)
            {
                canAttack = false;
                return; 
            }

            // Как только таймер равен 0 (в том числе на самом первом кадре игры),
            // мы проверяем, есть ли перед нами крыса. Если есть — мгновенно стреляем!
            if (CheckIsEnemyInRange())
            {
                canAttack = true;
            }
        }

        protected virtual void HandleAttack()
        {
            UpdateCanAttack();
            if (!canAttack) 
                return;

            canAttack = false;
            attackTimer = cooldown; // Заводим кулдаун (например, на 2 секунды)

            // Спавним пулю
            var position = transform.position;
            position.x += spriteRenderer.bounds.extents.x + 0.1f; 

            GameObject bullet = Instantiate(simpleBullet, position, Quaternion.identity);
            
            if (bullet.TryGetComponent<Bullet>(out Bullet bulletScript))
            {
                bulletScript.SetDamage(bulletDamage);
            }

            // Включаем визуал открытого рта
            if (shootSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = shootSprite; // Открыли рот
                mouthTimer = changeDuration;         // Завели таймер закрытия рта
            }
        }
    }
}