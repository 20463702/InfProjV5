using System;
using Characters;
using Characters.Enemy1;
using Characters.PlayerCharacter;
using UnityEngine;
using UnityEngine.UIElements;

namespace Weaponry.Melee
{
    public class MeleeWeapon : AbstractWeapon
    {
        public MeleeWeapon(float d, float r)
        {
            Damage = d;
            Range = r;
        }
    }
}