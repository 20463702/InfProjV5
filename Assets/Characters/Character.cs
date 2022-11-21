using Items.Inventory;
using UnityEngine;
using Weaponry;

namespace Characters
{
    
    [RequireComponent(typeof(Rigidbody2D), typeof(InventoryContainer))]
    public class Character : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody;
        protected float Speed = 4f;
        public float Health { get; private set; }

        protected void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.gravityScale = 0;
            Rigidbody.freezeRotation = true;
        }
        
#region Character Movement

        protected virtual void Movement() { }

        /// <param name="valH">Horizontal Value</param>
        /// <param name="valV">Vertical Value</param>
        protected void Move(float valH, float valV) =>
            Rigidbody.velocity = new Vector2(valH * Speed, valV * Speed);
        
#endregion Character Movement

        public void TakeDamage(AbstractWeapon w) =>
            Health -= w.Damage;
    }
}
