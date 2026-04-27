using UnityEngine;

namespace Assets
{
    public class Egg: Food
    {
        [Header("Cостояния our EGG")]
        [SerializeField] private Sprite fullHealthSprite; 
        [SerializeField] private Sprite crackedSprite;   
        [SerializeField] private Sprite brokenSprite; 
        
        protected override void Awake()
        {
            base.Awake();
            if (spriteRenderer != null && fullHealthSprite != null)
            {
                spriteRenderer.sprite = fullHealthSprite;
            }
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage); 
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            if (spriteRenderer is null) 
                return;

            var healthPercentage = curHp / maxHp;

            switch (healthPercentage)
            {
                case > 0.6f:
                    spriteRenderer.sprite = fullHealthSprite;
                    break;
                case > 0.3f and <= 0.6f:
                {
                    if (crackedSprite is not null) 
                        spriteRenderer.sprite = crackedSprite;
                    break;
                }
                case <= 0.3f and > 0:
                {
                    if (brokenSprite is not null) 
                        spriteRenderer.sprite = brokenSprite;
                    break;
                }
            }
        }
    }
}