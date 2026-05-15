using System.Collections.Generic;
using Assets; 
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject currentFoodPrefab;
    [SerializeField] public bool isFull = false;

    [Header("Visual Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer; 
    private Sprite _originalSprite;

    [Header("Effects Settings")]
    [SerializeField] private List<Effect> effects = new List<Effect>();

    public List<Effect> Effects => effects;

    private void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) _originalSprite = spriteRenderer.sprite;
    }

    public void SetNewFood(GameObject foodPrefab)
    {
        if (isFull) return;

        Instantiate(foodPrefab, transform.position, Quaternion.identity, transform);
        isFull = true;
    }

    public void ChangeSprite(Sprite newSprite)
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
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