using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class ChocoEgg : Food
    {
        [Header("Настройки Киндер-Сюрприза")]
        [Tooltip("Список префабов еды, которая может появиться из яйца")]
        [SerializeField] private List<GameObject> possibleFoodPrefabs;

        [Header("Спрайты Состояний")]
        [Tooltip("Спрайты яйца, когда его наполовину съели крысы")]
        [SerializeField] private Sprite brokenEggSprite; 

        private bool isBrokenSpriteSet = false;
        private bool isDying = false; // Предохранитель от двойного вызова Die()

        protected override void Awake()
        {
            base.Awake(); // Инициализация базового класса Food

            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        private void Update()
        {
            // Если здоровья осталось меньше половины — переключаем визуал на надкусанный
            if (!isBrokenSpriteSet && brokenEggSprite != null && curHp <= maxHp / 2f)
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = brokenEggSprite;
                    isBrokenSpriteSet = true;
                }
            }
        }

        // Перехватываем смерть!
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Die()
        {
            // Если яйцо уже находится в процессе уничтожения — выходим
            if (isDying) return; 
            isDying = true;

            // Если яйцо знает свою клетку и список наполнен — отдаем приказ клетке
            if (currentCell != null && possibleFoodPrefabs != null && possibleFoodPrefabs.Count > 0)
            {
                currentCell.ReplaceFoodFromEgg(possibleFoodPrefabs);
            }

            // Вызываем уничтожение самого яйца через базовый класс Food (Destroy(gameObject))
            base.Die();
        }
    }
}