using System;
using UnityEngine;

namespace Characters.Enemy1
{
    public class Enemy : Character
    {
        public float damage;
        public GameObject enemy1;
        public Transform respawn;
        private Vector2 _movement;
        private Transform _target;

        private new void Start()
        {
            MaxHealth = health;
            _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        private void Update()
        {
            EnemyMovement();
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            col.collider.gameObject.TryGetComponent(out Character c);
            c.TakeDamage(Weapon);
        }
    
        /// <param name="attackDamage"></param>
        public void Damage(int attackDamage)
        {
            health -= damage;

            if (health <= 0f)
                Die();
        }

        private void Die()
        {
            enemy1.transform.position = respawn.position;  
            health = MaxHealth;
        }

        private void EnemyMovement()
        {
            if (Vector2.Distance(transform.position, _target.position) < 1.2f)
            {
                Speed = 0f;
            }
            else
            {
                Speed = 4f;
                transform.position = Vector2.MoveTowards(transform.position, _target.position, Speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, -2);
            }
        }
    }
}
