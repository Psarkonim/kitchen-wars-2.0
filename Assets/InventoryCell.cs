using UnityEngine;
using TMPro; 

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private int amount;
    [SerializeField] private bool isEmpty = true;
    
    [SerializeField] private TextMeshPro amountText;

    private SpriteRenderer spriteRenderer;
    private Vector2 currentSize;
    public GameManager gameManager;
    public GameObject Food => food;
    private Food foodComponent;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSize = transform.localScale;

        // ПОЛНОСТЬЮ УДАЛИЛИ ОТСЮДА ПОИСК GAMEMANAGER!
        // Теперь менеджер сам придет и запишет себя в переменную ниже.

        if (food != null)
        {
            foodComponent = food.GetComponent<Food>();
        }
        
        UpdateAmountDisplay(); 
    }

    public void IncreaseAmount() => amount += 1;
    private void UpdateAmountDisplay()
    {
        if (amountText)
        {
            amountText.text = amount.ToString();
            
            amountText.gameObject.SetActive(amount > 0);
        }
    }

    private void OnMouseDown()
    {
        ChooseFood();
    }

    private void ChooseFood()
    {
        if (amount <= 0 || foodComponent == null)
            return;

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
        amount--;
        
        // Проверяем, что food и компонент на месте, перед тем как менять спрайт
        if (foodComponent != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = foodComponent.inventoryPassiveSprite;
        }

        if (food != null)
        {
            foodComponent = food.GetComponent<Food>();
        }

        UpdateAmountDisplay();  
        SetSpriteSize();
    }

    void Update()
    {
    }

    private void SetSpriteSize()
    {
        transform.localScale = new Vector3(currentSize.x, currentSize.y, 1f);
    }
}