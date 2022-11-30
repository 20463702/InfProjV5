using Characters.PlayerCharacter;
using UnityEngine;

namespace Characters.Enemy1
{
    public class Enemy : Character
    {
        public static LayerMask Layer;
        public float damage;

        private new void Start()
        {
            Layer = LayerMask.NameToLayer("Enemies");
            MaxHealth = health;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            col.collider.gameObject.TryGetComponent(out Character c);
            c.health -= damage;
        }
        
    }
}
