using Characters.PlayerCharacter;
using System;
using UnityEngine;
using Weaponry.Melee;

namespace Characters.Enemy
{
    public class Enemy : Character
    {
        public static LayerMask Layer;
        private Vector2 _movement;

        protected override void Start()
        {
            base.Start();
            MaxHealth = Health;
            Speed = Speed == 0f ? 4.0f : Speed;
        }

        
        protected override void Update()
        {
            base.Update();
            
            EnemyMovement();
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            if (col.collider.gameObject.TryGetComponent<PlayerCharacter.PlayerCharacter>(out _))
                Attack<PlayerCharacter.PlayerCharacter>();
        }

        private void EnemyMovement()
        {
            if (Vector2.Distance(transform.position, PlayerCharacter.PlayerCharacter.PlayerRef.transform.position) < 1.2f)
            {
                Speed = 0f;
            }
            else
            {
                Speed = 4f;
                transform.position = Vector2.MoveTowards(transform.position, PlayerCharacter.PlayerCharacter.PlayerRef.transform.position, Speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, -2);
            }
        }
    }
}
