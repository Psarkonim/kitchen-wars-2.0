using UnityEngine;

public class ToothpickRat : BasicRat 
{
    [SerializeField] private GameObject mouseToSpawn;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent<Orange>(out Orange _))
        {
            Instantiate(mouseToSpawn, rb.position + new Vector2(-1.1f, 0), Quaternion.identity);
            Die();
        }
    }
}
