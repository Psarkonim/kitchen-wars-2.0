using UnityEngine;

public class Pancake : Food
{
    [Header("Pancake Settings")]
    [SerializeField] private float knockbackSpeed = 6f;    
    [SerializeField] private float knockbackDuration = 0.5f; 
    [SerializeField] private int maxBounces = 3;             
    private int _bouncesLeft;

    protected override void Awake()
    {
        base.Awake();
        _bouncesLeft = maxBounces;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BasicRat>(out BasicRat rat))
        {
            if (_bouncesLeft > 0 && !rat.IsKnockedBack)
                BounceRat(rat);
        }
    }

    private void BounceRat(BasicRat rat)
    {
        _bouncesLeft--;

        KnockbackEffect knockback = new KnockbackEffect(knockbackSpeed, Vector2.right, knockbackDuration);
        
        knockback.ApplyEffect(rat);
        
        // Чтобы крыса сама его удалила через свой встроенный цикл HandleEffect, 
        // нам нужно прокинуть этот эффект в её приватный список. 
        // Давай сделаем это хаком или добавим публичный метод в крысу. 
        // Самый простой способ — запустить корутину прямо на оладушке, чтобы не переписывать логику списков крысы:
        StartCoroutine(DynamicKnockbackRoutine(rat, knockback));
        //вот показываю почему нейронка решила так сделать, потому что я тоже в шоке
        
        if (_bouncesLeft <= 0)
            Die();
    }

    private System.Collections.IEnumerator DynamicKnockbackRoutine(BasicRat rat, KnockbackEffect effect)
    {
        var elapsed = 0f;
        while (elapsed < knockbackDuration && rat != null)
        {
            effect.ApplyEffect(rat);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (rat != null)
            effect.RemoveEffect(rat);
    }
}