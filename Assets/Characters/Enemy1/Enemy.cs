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
        public float speed;
        private Transform _target;

        private new void Start()
        {
            MaxHealth = health;
            _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            col.collider.gameObject.TryGetComponent(out Character c);
            c.health -= damage;
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
        
        
    }
}
