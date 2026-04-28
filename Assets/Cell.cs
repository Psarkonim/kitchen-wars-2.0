using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private bool isEmpty;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private GameObject currentFood;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        isEmpty = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
    }

    void Update()
    {
        UpdateFood();
    }

    private void UpdateFood()
    {
        if (currentFood.IsDestroyed() && !isEmpty)
        {
            foodPrefab = null;
            isEmpty = true;
        }
        else if (isEmpty && !(foodPrefab is null))
        {
            isEmpty = false;
            var position = transform.position;
            currentFood = Instantiate(foodPrefab, position, Quaternion.identity);
        }
    }

    public void SetNewFood(GameObject food)
    {
        if (isEmpty)
            foodPrefab = food;
    }
}
