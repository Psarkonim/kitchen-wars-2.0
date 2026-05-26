using Assets;
using UnityEngine;

public class KnockbackEffect : Effect
{
    private float _knockbackSpeed;
    private Vector2 _direction;
    private Rigidbody2D _ratRb;

    public KnockbackEffect(float knockbackSpeed, Vector2 direction, float duration) : base(duration)
    {
        _knockbackSpeed = knockbackSpeed;
        _direction = direction.normalized;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void ApplyEffect(BasicRat rat)
    {
        _ratRb = rat.GetComponent<Rigidbody2D>();
        
        //возможно тут нам надо будет останавливать движение крысы на момент отталкивания, но без затеста я хз как там будет
        
        
        if (_ratRb != null)
            _ratRb.linearVelocity = _direction * _knockbackSpeed;
    }

    public override void RemoveEffect(BasicRat rat)
    {
        if (_ratRb != null)
            _ratRb.linearVelocity = Vector2.zero;
    }
}