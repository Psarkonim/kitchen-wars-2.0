using System.Collections.Generic;
using UnityEngine;
using Assets; // Для доступа к Малине

public class ChocolateCocktail : Food
{
    [Header("Настройки Ауры Урона")]
    [SerializeField] private float buffRadius = 3f;          
    [SerializeField] private float damageBuffMultiplier = 1.5f; 
    
    private List<Chocolate> _buffedChocolates = new List<Chocolate>();
    private List<Raspberry> _buffedRaspberries = new List<Raspberry>();

    protected override void Awake()
    {
        base.Awake();
        InvokeRepeating(nameof(ApplyDamageBuffAura), 0.5f, 0.5f);
    }

    private void ApplyDamageBuffAura()
    {
        Food[] allFoodOnScene = FindObjectsByType<Food>(FindObjectsSortMode.None);
        
        List<Chocolate> chocolatesInRadius = new List<Chocolate>();
        List<Raspberry> raspberriesInRadius = new List<Raspberry>();

        foreach (Food ally in allFoodOnScene)
        {
            if (ally.gameObject == this.gameObject) continue;

            float distance = Vector2.Distance(transform.position, ally.transform.position);

            if (distance <= buffRadius)
            {
                // 1. Усиливаем Шоколад
                if (ally.TryGetComponent<Chocolate>(out Chocolate chocolate))
                {
                    chocolatesInRadius.Add(chocolate);
                    if (!_buffedChocolates.Contains(chocolate))
                    {
                        chocolate.bulletDamage *= damageBuffMultiplier; 
                        _buffedChocolates.Add(chocolate);
                        SetBuffColor(chocolate.spriteRenderer, true);
                    }
                }
                
                else if (ally.TryGetComponent<Raspberry>(out Raspberry raspberry))
                {
                    raspberriesInRadius.Add(raspberry);
                    if (!_buffedRaspberries.Contains(raspberry))
                    {
                        raspberry.bulletDamage *= damageBuffMultiplier;
                        _buffedRaspberries.Add(raspberry);
                        SetBuffColor(raspberry.spriteRenderer, true);
                    }
                }
            }
        }

        for (int i = _buffedChocolates.Count - 1; i >= 0; i--)
        {
            Chocolate choc = _buffedChocolates[i];
            if (choc == null || !chocolatesInRadius.Contains(choc))
            {
                if (choc != null)
                {
                    choc.bulletDamage /= damageBuffMultiplier; 
                    SetBuffColor(choc.spriteRenderer, false);
                }
                _buffedChocolates.RemoveAt(i);
            }
        }

        for (int i = _buffedRaspberries.Count - 1; i >= 0; i--)
        {
            Raspberry rasp = _buffedRaspberries[i];
            if (rasp == null || !raspberriesInRadius.Contains(rasp))
            {
                if (rasp != null)
                {
                    rasp.bulletDamage /= damageBuffMultiplier; 
                    SetBuffColor(rasp.spriteRenderer, false);
                }
                _buffedRaspberries.RemoveAt(i);
            }
        }
    }

    private void SetBuffColor(SpriteRenderer sr, bool isBuffed)
    {
        if (sr != null)
        {
            // Окрашиваем усиленного стрелка в шоколадно-коричневый/светло-оранжевый оттенок
            sr.color = isBuffed ? new Color(0.8f, 0.5f, 0.3f) : Color.white;
        }
    }

    public override void Die()
    {
        foreach (Chocolate choc in _buffedChocolates)
        {
            if (choc != null)
            {
                choc.bulletDamage /= damageBuffMultiplier;
                SetBuffColor(choc.spriteRenderer, false);
            }
        }
        foreach (Raspberry rasp in _buffedRaspberries)
        {
            if (rasp != null)
            {
                rasp.bulletDamage /= damageBuffMultiplier;
                SetBuffColor(rasp.spriteRenderer, false);
            }
        }
        base.Die();
    }

    private void OnDrawGizmos()
    {
        // Коричневый круг зоны баффа урона в окне Scene
        Gizmos.color = new Color(0.5f, 0.25f, 0f, 0.2f);
        Gizmos.DrawWireSphere(transform.position, buffRadius);
    }
}