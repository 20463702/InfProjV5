
using UnityEngine;

namespace Weaponry
{
    public abstract class AbstractWeapon
    {
        public float Damage { get; protected set; }
        public float Range { get; protected set; }
        public float DeltaTimeBetweenAttacks { get; protected set; }
        public float TimeBetweenAttacks { get; protected set; }

        public void UpdateDeltaTime()
        {
            DeltaTimeBetweenAttacks = DeltaTimeBetweenAttacks > 0 ? DeltaTimeBetweenAttacks - Time.deltaTime : 0;
        }

        public void ResetDeltaTime()
        {
            DeltaTimeBetweenAttacks = TimeBetweenAttacks;
        }
    }
}
