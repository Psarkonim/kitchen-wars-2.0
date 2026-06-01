using System.Collections;
using System.Collections.Generic;
using Assets; 
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject currentFoodPrefab;
    [SerializeField] public bool isFull = false;
    private GameObject currentFood;

    [Header("Visual Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer; 
    private Sprite _originalSprite;

    [Header("Effects Settings")]
    [SerializeField] private List<Effect> effects = new List<Effect>();

    public List<Effect> Effects => effects;
    public GameObject CurrentFoodPrefab => currentFoodPrefab;

    private void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) _originalSprite = spriteRenderer.sprite;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void SetNewFood(GameObject foodPrefab)
    {
        if (isFull) return;

        currentFood = Instantiate(foodPrefab, transform.position, Quaternion.identity, transform);
        currentFoodPrefab = foodPrefab;
        isFull = true;

        // Ищем компонент Food не только на верхнем уровне, но и внутри префаба (на всякий случай)
        Food foodScript = currentFood.GetComponentInChildren<Food>();
        
        if (foodScript != null)
        {
            foodScript.currentCell = this;
            Debug.Log($"[Cell] Ссылка на клетку успешно передана в объект: {currentFood.name}");
        }
        else
        {
            Debug.LogError($"[Cell] КРИТИЧЕСКАЯ ОШИБКА: На префабе {foodPrefab.name} не найден скрипт Food или ChocoEgg!");
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void SetNewRecipeFood(GameObject foodPrefab)
    {
        Destroy(currentFood);
        currentFood = Instantiate(foodPrefab, transform.position, Quaternion.identity, transform);
        currentFoodPrefab = foodPrefab;
        isFull = true;

        Food foodScript = currentFood.GetComponentInChildren<Food>();
        
        if (foodScript != null)
        {
            foodScript.currentCell = this;
            Debug.Log($"[Cell] Ссылка на клетку успешно передана при крафте в: {currentFood.name}");
        }
        else
        {
            Debug.LogError($"[Cell] КРИТИЧЕСКАЯ ОШИБКА: При крафте на префабе {foodPrefab.name} не найден скрипт Food!");
        }
    }

    public void ReplaceFoodFromEgg(List<GameObject> prefabsList)
    {
        Debug.Log($"[Cell] Метод подмены вызван! В списке префабов: {prefabsList.Count} шт.");
        
        if (prefabsList == null || prefabsList.Count == 0) return;

        int randomIndex = Random.Range(0, prefabsList.Count);
        GameObject chosenPrefab = prefabsList[randomIndex];

        if (chosenPrefab == null) 
        {
            Debug.LogError("[Cell] Выбранный случайный префаб равен null!");
            return;
        }

        if (currentFood != null)
        {
            // Временно закомментируй Destroy, чтобы проверить, появится ли объект рядом
            Destroy(currentFood); 
        }

        currentFood = Instantiate(chosenPrefab, transform.position, Quaternion.identity, transform);
        currentFoodPrefab = chosenPrefab;
        isFull = true;

        Debug.Log($"[Cell] Объект {currentFood.name} успешно заспавнен в иерархии клетки!");

        if (currentFood.TryGetComponent<Food>(out Food foodScript))
        {
            foodScript.currentCell = this;
        }

        StartCoroutine(AnimateFoodSpawn(currentFood));
    }

    private System.Collections.IEnumerator AnimateFoodSpawn(GameObject foodObject)
    {
        if (foodObject == null) yield break;

        // Находим коллайдер и отключаем его на время анимации, 
        // чтобы крысы на этой клетке не сожрали малинку в первую же миллисекунду!
        Collider2D foodCollider = foodObject.GetComponent<Collider2D>();
        if (foodCollider != null)
        {
            foodCollider.enabled = false;
        }

        // Находим SpriteRenderer, чтобы плавно проявить объект через прозрачность (Alpha),
        // если вдруг с масштабом (Scale) происходят конфликты из-за других скриптов
        SpriteRenderer foodRenderer = foodObject.GetComponent<SpriteRenderer>();
        Color originalColor = foodRenderer != null ? foodRenderer.color : Color.white;

        Vector3 targetScale = foodObject.transform.localScale; 
        foodObject.transform.localScale = Vector3.zero; // Сжимаем в ноль

        float elapsedTime = 0f;
        float duration = 0.3f; // Длительность появления (чуть длиннее, чтобы заметить)

        while (elapsedTime < duration)
        {
            if (foodObject == null) yield break; // Защита от удаления

            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // Плавное увеличение масштаба
            float scaleModifier = Mathf.Sin(progress * Mathf.PI * 0.5f); 
            foodObject.transform.localScale = targetScale * scaleModifier;

            // Плавное проявление из прозрачности
            if (foodRenderer != null)
            {
                foodRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, progress);
            }

            yield return null;
        }

        // Возвращаем финальные идеальные параметры
        if (foodObject != null)
        {
            foodObject.transform.localScale = targetScale;
            if (foodRenderer != null) foodRenderer.color = originalColor;
            
            if (foodCollider != null)
            {
                foodCollider.enabled = true; // Возвращаем физику крысам
                Debug.Log($"[Cell] Сюрприз {foodObject.name} успешно вырос и готов к бою!");
            }
        }
    }
    public void ChangeSprite(Sprite newSprite)
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    public void Update()
    {
        if (isFull && currentFood == null)
        {
            currentFoodPrefab = null;
            isFull = false;
        }
    }

    public void ResetSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = _originalSprite;
        }
    }

    public void AddEffect(Effect newEffect)
    {
        if (!effects.Exists(e => e.GetType() == newEffect.GetType()))
        {
            effects.Add(newEffect);
            Debug.Log($"На клетку {gameObject.name} наложен эффект: {newEffect.GetType().Name}");
        }
    }

    public void RemoveEffectsByType<T>() where T : Effect
    {
        effects.RemoveAll(e => e is T);
        Debug.Log($"С клетки {gameObject.name} удалены все эффекты типа: {typeof(T).Name}");
    }

    public void RemoveEffect(Effect effect)
    {
        if (effects.Contains(effect))
        {
            effects.Remove(effect);
        }
    }

    public bool HasEffect<T>() where T : Effect
    {
        return effects.Exists(e => e is T);
    }
}