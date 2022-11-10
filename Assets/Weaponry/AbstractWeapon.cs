using Characters;
using Items;

namespace Weaponry
{
    public abstract class AbstractWeapon : Item
    {
        public float Damage { get; protected set; }
        private int _rechargeTime;

        /// <param name="t">Target</param>
        protected abstract void DamageTarget(Character t);
    }
}
