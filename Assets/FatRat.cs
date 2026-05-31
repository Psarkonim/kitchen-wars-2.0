using UnityEditor.Animations;
using UnityEngine;

public class FatRat : BasicRat
{
    [SerializeField] private int bulletsIn;
    [SerializeField] private int bulletsToExplode;
    [SerializeField] private Sprite firstStage;
    [SerializeField] private Sprite secondStage;
    [SerializeField] private Sprite thirdStage;
    [SerializeField] private GameObject explosionPrefab;

    SpriteRenderer spriteRenderer;
    Animator animator;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out Bullet _))
        {
            bulletsIn += 1;
            animator.SetInteger("BulletIn", bulletsIn);

            if (bulletsIn == 3)
            {
                spriteRenderer.sprite = secondStage;
            }
            else if (bulletsIn == 6)
            {
                spriteRenderer.sprite = thirdStage;
            }
            else if (bulletsIn == 8)
            {
                var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosion.GetComponent<Explosion>().Detonate();
                Die();
            }
        }
    }
}
