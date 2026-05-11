using Assets;
using UnityEngine;

public class SlowEffect : Effect
{
    private float slowFactor; // Например, 0.5f для замедления в 2 раза
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
        // Находим приватное поле curSpeed через Reflection, 
        // так как в твоем классе Rat оно private и нет публичного сеттера
        var field = typeof(Rat).GetField("curSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        
        if (field != null)
        {
            float currentSpeed = (float)field.GetValue(rat);
            field.SetValue(rat, currentSpeed * slowFactor);
        }

        // Логика удаления эффекта по истечении времени
        if (Time.time > startTime + duration)
        {
            // Чтобы это работало, в классе Rat список effects должен быть public 
            // или иметь метод для удаления. 
            // Но для "бесконечной" муки на линии duration можно сделать очень большим.
        }
    }
}