using UnityEngine;

public class ToothpickRat : BasicRat 
{
    [SerializeField] private GameObject mouseToSpawn;
    [SerializeField] private AudioClip fallSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent<Orange>(out Orange _))
        {
            Instantiate(mouseToSpawn, rb.position + new Vector2(-1.1f, 0), Quaternion.identity);
            Die();
            AudioSource.PlayClipAtPoint(fallSound, transform.position, PlayerPrefs.GetFloat("SoundVolume"));
        }
    }
}
