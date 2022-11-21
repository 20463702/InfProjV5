using System;
using Characters;
using Characters.PlayerCharacter;
using UnityEngine;
using UnityEngine.UIElements;

namespace Weaponry.Melee
{
    public class MeleeWeapon : AbstractWeapon
    {
        public MeleeWeapon(float d, float r, float t)
        {
            Damage = d;
            Range = r;
            TimeBetweenAttacks = t;
            DeltaTimeBetweenAttacks = 0f;
        }
    }
}