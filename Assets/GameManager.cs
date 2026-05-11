using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject currentFood;
    public InventoryCell activeCell;

    [SerializeField] public Transform cells;
    [SerializeField] public LayerMask cellMask;


    private void Update()
    {
        if (currentFood == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, cellMask);

            if (hit.collider && currentFood)
            {
                Cell cell = hit.collider.GetComponent<Cell>();

                if (cell != null)
                {
                    cell.SetNewFood(currentFood);

                    if (activeCell != null)
                    {
                        activeCell.ConsumeFood();
                        activeCell = null; 
                    }

                    currentFood = null;
                }
            }
        }
    }
}