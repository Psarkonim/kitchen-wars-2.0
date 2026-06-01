using System.Collections.Generic;
using UnityEngine;
using Assets; 

public class RaspberryCocktail : Food
{
    [Header("Настройки Ауры Скорости")]
    [SerializeField] private float buffRadius = 3f;         
    [SerializeField] private float speedBuffMultiplier = 0.5f;

    private List<Chocolate> _buffedChocolates = new List<Chocolate>();
    private List<Raspberry> _buffedRaspberries = new List<Raspberry>();

    protected override void Awake()
    {
        base.Awake();
        InvokeRepeating(nameof(ApplySpeedBuffAura), 0.5f, 0.5f);
    }

    private void ApplySpeedBuffAura()
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
                if (ally.TryGetComponent<Chocolate>(out Chocolate chocolate))
                {
                    chocolatesInRadius.Add(chocolate);
                    if (!_buffedChocolates.Contains(chocolate))
                    {
                        chocolate.fireRate *= speedBuffMultiplier; 
                        _buffedChocolates.Add(chocolate);
                        SetBuffColor(chocolate.spriteRenderer, true);
                        Debug.Log($"[КОКТЕЙЛЬ] Ускорил темп стрельбы Шоколада: {chocolate.name}");
                    }
                }
                else if (ally.TryGetComponent<Raspberry>(out Raspberry raspberry))
                {
                    raspberriesInRadius.Add(raspberry);
                    if (!_buffedRaspberries.Contains(raspberry))
                    {
                        raspberry.cooldown *= speedBuffMultiplier;
                        _buffedRaspberries.Add(raspberry);
                        SetBuffColor(raspberry.spriteRenderer, true);
                        Debug.Log($"[КОКТЕЙЛЬ] Ускорил темп стрельбы Малины: {raspberry.name}");
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
                    choc.fireRate /= speedBuffMultiplier; 
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
                    rasp.cooldown /= speedBuffMultiplier; 
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
            sr.color = isBuffed ? new Color(1f, 0.6f, 0.8f) : Color.white;
        }
    }

    public override void Die()
    {
        foreach (Chocolate choc in _buffedChocolates)
        {
            if (choc != null)
            {
                choc.fireRate /= speedBuffMultiplier;
                SetBuffColor(choc.spriteRenderer, false);
            }
        }
        foreach (Raspberry rasp in _buffedRaspberries)
        {
            if (rasp != null)
            {
                rasp.cooldown /= speedBuffMultiplier;
                SetBuffColor(rasp.spriteRenderer, false);
            }
        }
        base.Die();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0.5f, 0.2f);
        Gizmos.DrawWireSphere(transform.position, buffRadius);
    }
}