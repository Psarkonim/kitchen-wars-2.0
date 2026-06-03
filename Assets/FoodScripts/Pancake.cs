using System.Runtime.CompilerServices;
using UnityEngine;

public class Pancake : Food
{
    [Header("Pancake Settings")]
    [SerializeField] private float knockbackSpeed = 8f;    
    [SerializeField] private float knockbackDuration = 0.4f; 
    [SerializeField] private int maxBounces = 3;
    [SerializeField] private AudioClip bounceSound;

    private int _bouncesLeft;
    private bool _isBouncing;

    protected override void Awake()
    {
        base.Awake();
        _bouncesLeft = maxBounces;
    }

    public override void TakeDamage(float damage)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isBouncing) return;

        if (collision.TryGetComponent<BasicRat>(out BasicRat rat))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (_bouncesLeft > 0 && !rat.IsKnockedBack)
                {
                    StartCoroutine(BounceSequence(rat));
                    AudioSource.PlayClipAtPoint(bounceSound, transform.position, PlayerPrefs.GetFloat("SoundVolume"));
                }
            }
        }
    }

    private System.Collections.IEnumerator BounceSequence(BasicRat rat)
    {
        _isBouncing = true;
        _bouncesLeft--;


        Vector2 pushDirection = Vector2.right; 
        rat.TakeKnockback(pushDirection * knockbackSpeed, knockbackDuration);
        
        yield return new WaitForSeconds(0.2f);
        _isBouncing = false;

        if (_bouncesLeft <= 0)
        {
            Destroy(gameObject); 
        }
    }
}