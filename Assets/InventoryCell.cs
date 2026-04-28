using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private int amount;
    [SerializeField] private bool isEmpty = true;

    protected SpriteRenderer spriteRenderer;
    private Vector2 currentSize;
    public GameManager gameManager; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSize = transform.localScale;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        ChooseFood();
    }

    private void ChooseFood()
    {
        var foodComponent = food.GetComponent<Food>();

        if (spriteRenderer.sprite == foodComponent.inventoryActiveSprite)
        {
            gameManager.currentFood = null;
            spriteRenderer.sprite = foodComponent.inventoryPassiveSprite;
        }
        else
        {
            gameManager.currentFood = food;
            spriteRenderer.sprite = foodComponent.inventoryActiveSprite;
        }

        SetSpriteSize();
    }

    void Update()
    {
        if (amount == 0 && !isEmpty)
        {
            food = null;
            isEmpty = true;
        }
        else if (isEmpty && !(food is null))
        {
            isEmpty = false;
            var position = transform.position;
            spriteRenderer.sprite = food.GetComponent<Food>().inventoryPassiveSprite;
            SetSpriteSize();
        }

        if (gameManager.currentFood is null)
        {
            spriteRenderer.sprite = food.GetComponent<Food>().inventoryPassiveSprite;
            SetSpriteSize();
        }
        
    }

    private void SetSpriteSize()
    {
        transform.localScale = new Vector3(currentSize.x, currentSize.y, 1f);
    }
}