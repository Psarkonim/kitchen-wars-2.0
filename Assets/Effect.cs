using System.Collections;
using UnityEngine;

namespace Assets
{
    public class Effect
    {
        private float duration;
        private float startTime;

        public bool IsEnded => Time.time > startTime + duration;

        public Effect(float duration)
        {
            this.duration = duration;
            startTime = Time.time;
        }

        public virtual void ApplyEffect(BasicRat rat)
        {
            throw new System.Exception();
        }

        public virtual void RemoveEffect(BasicRat rat)
        {
            throw new System.Exception();
        }

        public void Refresh()
        {
            startTime = Time.time;
        }
    }
}