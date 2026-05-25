using UnityEngine;

public class FoodReward : MonoBehaviour
{
    [SerializeField] public GameObject Food;

    public GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        var inventory = gameManager.Inventory.GetComponent<Inventory>();
        
        foreach (var item in inventory.Cells)
        {
            if (item.Food.name == Food.name)
            {
                item.IncreaseAmount();
                Destroy(gameObject);
            }
        }
    }
}
