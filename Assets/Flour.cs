using System.Collections.Generic;
using Assets;
using UnityEngine;

public class Flour : Food
{
    [Header("Flour Settings")]
    [SerializeField] private float slowMultiplier = 0.5f;
    [SerializeField] private float laneLength = 15f;
    [SerializeField] private LayerMask cellLayer;

    private List<Cell> _affectedCells = new(); 
    private Sprite _flourSprite; 

    private void Start()
    {
        ExtractSpriteFromPrefab();
        ApplyFlourToLane();
    }

    private void ExtractSpriteFromPrefab()
    {
        if (currentFoodPrefab != null)
        {
            var sr = currentFoodPrefab.GetComponent<SpriteRenderer>();
            if (sr != null) _flourSprite = sr.sprite;
        }
    }

    private void ApplyFlourToLane()
    {
        var size = new Vector2(laneLength, 0.5f);
        var center = (Vector2)transform.position + new Vector2(laneLength / 2f, 0);

        var cellColliders = Physics2D.OverlapBoxAll(center, size, 0f, cellLayer);

        foreach (var col in cellColliders)
        {
            var cell = col.GetComponent<Cell>();
            if (cell != null)
            {
                if (_flourSprite != null) 
                    cell.ChangeSprite(_flourSprite);

                cell.AddEffect(new SlowEffect(slowMultiplier, 9999f));

                _affectedCells.Add(cell);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var cell in _affectedCells)
        {
            if (cell != null)
            {
                cell.ResetSprite();
                cell.RemoveEffectsByType<SlowEffect>();
            }
        }
        _affectedCells.Clear();
    }
}