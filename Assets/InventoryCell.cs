using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private int amount;
    [SerializeField] private bool isEmpty = true;

    protected SpriteRenderer spriteRenderer;
    private Vector2 currentSize;
    public GameManager gameManager;

    private Food foodComponent;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSize = transform.localScale;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (food != null)
        {
            foodComponent = food.GetComponent<Food>();
        }
    }

    private void OnMouseDown()
    {
        ChooseFood();
    }

    private void ChooseFood()
    {
        if (amount == 0 || foodComponent == null) return;

        if (spriteRenderer.sprite == foodComponent.inventoryActiveSprite)
        {
            gameManager.currentFood = null;
            gameManager.activeCell = null;
            spriteRenderer.sprite = foodComponent.inventoryPassiveSprite;
        }
        else if (gameManager.activeCell == null)
        {
            gameManager.currentFood = food;
            gameManager.activeCell = this; 
            spriteRenderer.sprite = foodComponent.inventoryActiveSprite;
        }

        SetSpriteSize();
    }

    public void ConsumeFood()
    {
        Debug.Log("Еда поставлена");
        spriteRenderer.sprite = foodComponent.inventoryPassiveSprite;
        amount--;
        SetSpriteSize();
    }

    void Update()
    {
        if (isEmpty && food != null)
        {
            isEmpty = false;
            foodComponent = food.GetComponent<Food>();
            spriteRenderer.sprite = foodComponent.inventoryPassiveSprite;
            SetSpriteSize();
        }
    }

    private void SetSpriteSize()
    {
        transform.localScale = new Vector3(currentSize.x, currentSize.y, 1f);
    }
}