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
        Debug.Log("Collide");
        foreach (var item in inventory.Cells)
        {
            Debug.Log(item.Food.name);
            if (ReferenceEquals(item.Food, Food))
            {
                Debug.Log("Match");
                item.IncreaseAmount();
                Destroy(gameObject);
            }
        }
    }
}
