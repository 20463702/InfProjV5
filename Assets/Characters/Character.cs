using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Characters
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody;
        [field: NonSerialized]
        public List<Item> InventoryItems { get; protected set; }
        protected float Speed;

        protected void Start()
        {
            this.Rigidbody = GetComponent<Rigidbody2D>();
            this.Rigidbody.gravityScale = 0;
            this.InventoryItems = new List<Item>();
            this.Speed = 5f;
        }

        public virtual Item GiveItem(Item i)
        {
            this.InventoryItems.Add(i);
            return i;
        }

#region Character Movement

        protected virtual void Movement() { }

        /// <param name="valH">Horizontal Value</param>
        /// <param name="valV">Vertical Value</param>
        protected void Move(float valH, float valV) =>
            this.Rigidbody.velocity = new Vector2(valH * this.Speed, valV * this.Speed);
        
#endregion Character Movement
    }
}
