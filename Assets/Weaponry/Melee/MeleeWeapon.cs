using Characters;
using Characters.PlayerCharacter;
using UnityEngine;

namespace Weaponry.Melee
{
    public class MeleeWeapon : AbstractWeapon
    {
        private float _range;

        protected override void DamageTarget(Character t)
        {
            if (Vector3.Distance(PlayerCharacter.PlayerRef.transform.position, t.transform.position) > _range)
                return;
            t.TakeDamage(this);
        }
    }
}
