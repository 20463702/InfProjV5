using Characters;

namespace Weaponry
{
    public abstract class AbstractWeapon
    {
        public float Damage { get; protected set; }
        private int _rechargeTime;
    }
}
