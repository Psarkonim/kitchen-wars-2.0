using Assets;
using UnityEngine;

public class Flour : Food
{
    [Header("Flour Settings")]
    [SerializeField] private float slowMultiplier = 0.5f;
    [SerializeField] private GameObject flourVisualPrefab; 
    [SerializeField] private float laneLength = 15f; 
    [SerializeField] private LayerMask cellLayer; // Нужно добавить слой клеток

    private void Start()
    {
        SpawnVisuals();
        ApplyEffectToCells(); // Теперь работаем с клетками
    }

    private void SpawnVisuals()
    {
        for (var i = 0; i < laneLength; i++)
        {
            var spawnPos = transform.position + new Vector3(i, 0, 0);
            Instantiate(flourVisualPrefab, spawnPos, Quaternion.identity, transform);
        }
    }

    private void ApplyEffectToCells()
    {
        // 1. Создаем зону поиска вдоль линии
        // transform.position - это где стоит пачка муки
        Vector2 size = new Vector2(laneLength, 0.5f);
        Vector2 center = (Vector2)transform.position + new Vector2(laneLength / 2f, 0);

        // 2. Ищем все коллайдеры на слое Cell
        Collider2D[] cellColliders = Physics2D.OverlapBoxAll(center, size, 0f, cellLayer);

        foreach (var col in cellColliders)
        {
            Cell cell = col.GetComponent<Cell>();
            if (cell != null)
            {
                // Накладываем эффект на КЛЕТКУ
                cell.AddEffect(new SlowEffect(slowMultiplier, 9999f));
            }
        }
    }
}