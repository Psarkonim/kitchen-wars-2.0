using UnityEngine;

public class ToothpickRat : BasicRat 
{
    [SerializeField] private GameObject mouseToSpawn;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Orange>(out Orange _))
        {
            Instantiate(mouseToSpawn, transform.position, Quaternion.identity);
            Die();
        }
    }
}
