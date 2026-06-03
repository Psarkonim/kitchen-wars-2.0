using UnityEngine;

namespace Assets
{
    public class ChocoRaspberry : Raspberry
    {
        [Header("Настройки Малины в Шоколаде")]
        [SerializeField] private GameObject chocoBulletPrefab; // Префаб шоколадной пули
        [SerializeField] private Sprite chocoShootSprite;      // СПРАЙТ открытого рта 
        [SerializeField] private float upgradedDamage = 8f;   

        protected override void Awake()
        {
            base.Awake(); 

            bulletDamage = upgradedDamage; 

            if (chocoBulletPrefab != null)
            {
                simpleBullet = chocoBulletPrefab;
            }

            if (chocoShootSprite != null)
            {
                shootSprite = chocoShootSprite;
            }
        }
    }
}