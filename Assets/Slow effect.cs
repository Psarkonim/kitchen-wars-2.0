using Assets;
using UnityEngine;

public class SlowEffect : Effect
{
    private float slowFactor; 
    private float duration;
    private float startTime;

    public SlowEffect(float slowFactor, float duration)
    {
        this.slowFactor = slowFactor;
        this.duration = duration;
        this.startTime = Time.time;
    }

    public override void ApplyEffect(Rat rat)
    {
        var field = typeof(Rat).GetField("curSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        
        if (field != null)
        {
            var currentSpeed = (float)field.GetValue(rat);
            field.SetValue(rat, currentSpeed * slowFactor);
        }

        if (!(Time.time > startTime + duration)) 
            return;
        var effectsField = typeof(Rat).GetField("effects", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (effectsField == null) 
            return;
        var effectsList = (System.Collections.Generic.List<Effect>)effectsField.GetValue(rat);
        effectsList.Remove(this);
        var speedField = typeof(Rat).GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var curSpeedField = typeof(Rat).GetField("curSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (speedField == null || curSpeedField == null) 
            return;
        var originalSpeed = (float)speedField.GetValue(rat);
        curSpeedField.SetValue(rat, originalSpeed);
    }
}