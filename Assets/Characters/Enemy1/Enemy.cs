using Characters.PlayerCharacter;
using System;
using UnityEngine;

namespace Characters.Enemy1
{
    public class Enemy : Character
    {
        public static LayerMask Layer;
        public float damage;
        private Vector2 _movement;
        private Transform _target;

        protected void Start()
        {
            base.Start();
            Layer = LayerMask.NameToLayer("Enemies");
            MaxHealth = health;
            Speed = Speed == 0f ? 4.0f : Speed;
        }
        
        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, Speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            col.collider.gameObject.TryGetComponent(out Character c);
            c.health -= damage;
        }
    }
}
