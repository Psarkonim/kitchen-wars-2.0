using Assets;
using UnityEngine;

public class Flour : Food
{
    [Header("Flour Settings")]
    [SerializeField] private float slowMultiplier = 0.5f;
    [SerializeField] private GameObject flourVisualPrefab; 
    [SerializeField] private float laneLength = 15f; 

    private void Start()
    {
        SpawnVisuals();
        ApplyEffectToLane();
    }

    private void SpawnVisuals()
    {
        for (var i = 0; i < laneLength; i++)
        {
            var spawnPos = transform.position + new Vector3(i, 0, 0);
            Instantiate(flourVisualPrefab, spawnPos, Quaternion.identity, transform);
        }
    }

    private void ApplyEffectToLane()
    {
        var colliders = Physics2D.OverlapBoxAll(
            transform.position + new Vector3(laneLength / 2, 0, 0), 
            new Vector2(laneLength, 1f), 
            0f, 
            enemyLayer
        );

        foreach (var col in colliders)
        {
            Rat rat = col.GetComponent<Rat>();
            if (rat != null)
            {
                AddSlowToRat(rat);
            }
        }
    }

    private void AddSlowToRat(Rat rat)
    {
        var effectsField = typeof(Rat).GetField("effects", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var effectsList = (System.Collections.Generic.List<Effect>)effectsField.GetValue(rat);
        
        effectsList.Add(new SlowEffect(slowMultiplier, 999f)); 
    }
}