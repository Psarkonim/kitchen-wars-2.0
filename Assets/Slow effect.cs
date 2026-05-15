using Assets;
using UnityEngine;

public class SlowEffect : Effect
{
    private float slowFactor; 

    public SlowEffect(float slowFactor, float duration): base(duration)
    {
        this.slowFactor = slowFactor;
    }
    public override void ApplyEffect(Rat rat)
    {
        rat.CurSpeed = rat.Speed * slowFactor;
    }

    public override void RemoveEffect(Rat rat)
    {
        rat.CurSpeed = rat.Speed;
    }
}