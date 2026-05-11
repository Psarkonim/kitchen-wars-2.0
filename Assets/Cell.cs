using System.Collections.Generic;
using Assets; 
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject currentFoodPrefab;
    [SerializeField] private bool isFull = false;

    [Header("Effects Settings")]
    [SerializeField] private List<Effect> effects = new List<Effect>();

    public List<Effect> Effects => effects;

    public void SetNewFood(GameObject foodPrefab)
    {
        if (isFull) return;

        Instantiate(foodPrefab, transform.position, Quaternion.identity, transform);
        isFull = true;
    }

    public void AddEffect(Effect newEffect)
    {
        if (!effects.Contains(newEffect))
        {
            effects.Add(newEffect);
            Debug.Log($"На клетку {gameObject.name} наложен эффект: {newEffect.GetType().Name}");
        }
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