using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using JetBrains.Annotations;
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
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.gravityScale = 0;
            Rigidbody.freezeRotation = true;
            InventoryItems = new List<Item>();
            Speed = 3f;
        }
        
        public int? ItemIndexInInventory(Item i)
        {
            foreach (var item in InventoryItems.Where(item => item.id == i.id))
                return InventoryItems.IndexOf(item);
            return null;
        }
        
        public virtual Item GiveItem(Item i, [CanBeNull] Character other)
        {
            if (other != null)
                _ = other.RemoveItem(i);

            var index = ItemIndexInInventory(i);
            if (index != null)
            {
                InventoryItems[(int)index].quantity++;
                return i;
            }

            InventoryItems.Add(i);

            return i;
        }

        public virtual Item RemoveItem(Item i)
        {
            InventoryItems.Remove(i);
            return i;
        }

#region Character Movement

        protected virtual void Movement() { }

        /// <param name="valH">Horizontal Value</param>
        /// <param name="valV">Vertical Value</param>
        protected void Move(float valH, float valV) =>
            Rigidbody.velocity = new Vector2(valH * Speed, valV * Speed);
        
#endregion Character Movement
    }
}
