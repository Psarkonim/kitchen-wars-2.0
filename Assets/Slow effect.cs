using Assets;
using UnityEngine;

public class SlowEffect : Effect
{
    private float slowFactor; 

    public SlowEffect(float slowFactor, float duration): base(duration)
    {
        this.slowFactor = slowFactor;
    }
    public override void ApplyEffect(BasicRat rat)
    {
        rat.CurSpeed = rat.Speed * slowFactor;
    }

    public override void RemoveEffect(BasicRat rat)
    {
        rat.CurSpeed = rat.Speed;
    }
}