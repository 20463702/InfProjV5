using Characters;
using Items.Inventory;
using Items;
using UnityEngine;

namespace Weaponry
{
    public abstract class AbstractWeapon
    {
        public float Damage { get; protected set; }
        public float Range { get; protected set; }
    }
}
