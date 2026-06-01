using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Параметры взрыва")]
    [SerializeField]public float explosionRadius;
    [SerializeField]public float explosionDamage;
    [SerializeField] public LayerMask damageableLayers;
    [SerializeField] private AudioClip explosionSound;

    [SerializeField] public GameObject visualEffectPrefab;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Detonate()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayers);

        foreach (Collider2D hit in hitColliders)
        {
            Debug.Log("hit");
            var food = hit.GetComponent<Food>();

            if (food != null)
            {
                Debug.Log("food hit");
                food.TakeDamage(explosionDamage);
            }

        }

        PlayExplosionEffects(); 

        Destroy(gameObject, 2f);
    }

    void PlayExplosionEffects()
    {
        Instantiate(visualEffectPrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(explosionSound, PlayerPrefs.GetFloat("SoundVolume"));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}