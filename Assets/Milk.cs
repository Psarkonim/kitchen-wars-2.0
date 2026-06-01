using UnityEngine;

public class Milk : Food
{
    [Header("Milk Healing Settings")]
    [SerializeField] private float healAmount = 10f;     
    [SerializeField] private float healInterval = 2f;    
    [SerializeField] private float healRadius = 4f;       

    [Header("Visual Effects")]
    [SerializeField] private GameObject healVisualPrefab; 

    private float _nextHealTime;

    protected override void Awake()
    {
        base.Awake(); 
        _nextHealTime = Time.time + healInterval; 
    }

    private void Update()
    {
        if (Time.time >= _nextHealTime)
        {
            HealNearbyFood();
            _nextHealTime = Time.time + healInterval; 
        }
    }

    private void HealNearbyFood()
    {
        Food[] allFoodOnScene = FindObjectsByType<Food>(FindObjectsSortMode.None);
        bool someoneHealed = false;

        foreach (Food allyFood in allFoodOnScene)
        {
            if (allyFood.gameObject == this.gameObject) continue;

            float distance = Vector2.Distance(transform.position, allyFood.transform.position);

            if (distance <= healRadius)
            {

                if (allyFood.CurHp < allyFood.MaxHpProperty)
                {
                    allyFood.Heal(healAmount);

                    if (allyFood.TryGetComponent<SpriteRenderer>(out SpriteRenderer targetSprite))
                    {
                        StartCoroutine(FlashGreen(targetSprite));
                    }

                    if (healVisualPrefab != null)
                    {
                        Vector3 spawnPos = allyFood.transform.position + new Vector3(0, 0.5f, 0);
                        Instantiate(healVisualPrefab, spawnPos, Quaternion.identity);
                    }

                }
            }
        }

    }

    private System.Collections.IEnumerator FlashGreen(SpriteRenderer sr)
    {
        sr.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        if (sr != null) sr.color = Color.white;
    }
}