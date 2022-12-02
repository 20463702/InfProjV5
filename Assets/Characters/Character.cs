using Items.Inventory;
using UnityEngine;
using Weaponry;
using Weaponry.Melee;

namespace Characters
{
    
    [RequireComponent(typeof(Rigidbody2D), typeof(InventoryContainer), typeof(BoxCollider2D))]
    public class Character : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody;
        protected float Speed = 4f;
        public float Health { get; private set; }
        [SerializeField] protected Transform respawn;
        public AbstractWeapon Weapon;
        protected float MaxHealth;
        
        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.gravityScale = 0;
            Rigidbody.freezeRotation = true;
            
            Health = Health > 0 ? Health : 100;
            MaxHealth = Health;
            
            Weapon ??= new MeleeWeapon(40f, 2f, 1f);
        }

        protected virtual void Update()
        {
            Weapon.UpdateDeltaTime();
        }

        protected virtual void Movement() { }

        /// <param name="valH">Horizontal Value</param>
        /// <param name="valV">Vertical Value</param>
        protected void Move(float valH, float valV) =>
            Rigidbody.velocity = new Vector2(valH * Speed, valV * Speed);
        
        public void TakeDamage(AbstractWeapon w)
        {
            Health -= w.Damage;
            if (Health <= 0f)
                Die();
        } 
        private void Die()
        {
            transform.position = respawn.position;  
            Health = MaxHealth;

            Start();
        }
        
        protected void Attack<T>() where T : Character
        {
            if (Weapon.DeltaTimeBetweenAttacks <= 0)
            {
                var colliders = Physics2D.OverlapCircleAll(transform.position, Weapon.Range);
                for (int i = 0, n = colliders.Length; i < n; i++)
                {
                    if (colliders[i].TryGetComponent<T>(out var c) 
                        && Vector3.Distance(transform.position, colliders[i].transform.position) <= Weapon.Range)
                        c.TakeDamage(Weapon);
                }
                Weapon.ResetDeltaTime();
            }
        }
    }
}
