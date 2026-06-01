using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    // Статическая ссылка на единственный экземпляр менеджера в игре
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // 1. Настраиваем синглтон (если делали на прошлом шаге)
        Instance = this;

        // 2. Находим ВСЕ ячейки инвентаря внутри объекта Inventory
        if (Inventory != null)
        {
            InventoryCell[] cellsInInventory = Inventory.GetComponentsInChildren<InventoryCell>(true);
            
            foreach (var cell in cellsInInventory)
            {
                cell.gameManager = this; // Вручную передаем СЕБЯ каждой ячейке!
                Debug.Log($"[GameManager] Успешно привязал себя к ячейке: {cell.gameObject.name}");
            }
        }
        else
        {
            Debug.LogError("[GameManager] Объект Inventory не привязан в Инспекторе!");
        }
    }
    public GameObject currentFood;
    public InventoryCell activeCell;

    [SerializeField] public GameObject Inventory;
    [SerializeField] public Transform cells;
    [SerializeField] public List<RatSpawner> spawners;
    [SerializeField] public LayerMask cellMask;
    [SerializeField] public LayerMask layerMask;
    [SerializeField] private List<Recipe> recipes = new List<Recipe>();

    private void HandlePlacement()
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
                    if (!cell.isFull)
                    {
                        cell.SetNewFood(currentFood);

                        if (activeCell != null)
                        {
                            activeCell.ConsumeFood();
                            activeCell = null;
                        }

                        currentFood = null;
                    }
                    else
                    {
                        foreach (var recipe in recipes)
                        {
                            if (recipe.TryMakeRecipe(currentFood, cell.CurrentFoodPrefab, out var result))
                            {

                                cell.SetNewRecipeFood(result);

                                if (activeCell != null)
                                {
                                    activeCell.ConsumeFood();
                                    activeCell = null;
                                }

                                currentFood = null;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private bool CheckIfLevelIsEnd()
    {
        BasicRat[] enemies = FindObjectsByType<BasicRat>(FindObjectsSortMode.None);

        if (spawners.All(spawner => spawner.IsSpawnEnd) && enemies.Length == 0) return true;

        return false;
    }

    private void Update()
    {
        var sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Substring(0, 5) != "Level") return;
        HandlePlacement();

        if (CheckIfLevelIsEnd())
        {
            var nextLevelNumber = int.Parse(sceneName.Last().ToString()) + 1;

            Debug.Log(nextLevelNumber);
            PlayerPrefs.SetInt("Current level", nextLevelNumber);
            PlayerPrefs.Save();
            SceneManager.LoadScene("WinVideo");
        }
    }
}