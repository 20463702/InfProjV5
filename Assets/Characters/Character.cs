using System;
using System.Collections.Generic;
using Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters
{
    
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class Character : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody;
        protected BoxCollider2D BoxCollider;
        [NonSerialized]
        public List<Item> Inventory;
        protected float Speed;

        private void Start()
        {
            this.Rigidbody = GetComponent<Rigidbody2D>();
            this.Rigidbody.gravityScale = 0;
            this.BoxCollider = GetComponent<BoxCollider2D>();
            this.Inventory = new List<Item>();
            this.Speed = 5f;
        }

#region CharacterMovement

        protected virtual void Movement() { }

        /// <param name="valH">Horizontal Value</param>
        /// <param name="valV">Vertical Value</param>
        protected void Move(float valH, float valV) =>
            this.Rigidbody.velocity = new Vector2(valH * this.Speed, valV * this.Speed);
        
#endregion // CharacterMovement
    }
}
