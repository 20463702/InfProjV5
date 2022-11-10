using System.Collections.Generic;
using System.Linq;
using Characters.Inventory.InventoryItem;
using Items;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters.Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        [SerializeField]
        protected GameObject invItemPrefab;
        protected GameObject _panel;
        [DoNotSerialize]
        public List<Item> Items { get; protected set; }

        private void Start()
        {
            _panel = transform.GetChild(0).gameObject;
            Items = new();
        }

        public void InvUpdate()
        {
            int y = 200;
            for (int i = 0, n = _panel.transform.childCount; i < n; i++)
                Destroy(_panel.transform.GetChild(i).gameObject);
            
            foreach (var item in Items)
            {
                var objRef = Instantiate(invItemPrefab, transform.GetChild(0));
                objRef.GetComponent<InvItem>().Set(item);

                objRef.transform.localPosition = new Vector3(0, y, 0);
                y -= 80;
            }
        }
        
        public int? IndexOfItem(Item i)
        {
            foreach (var item in Items.Where(item => item.id == i.id))
                return Items.IndexOf(item);
            return null;
        }
        
        public Item AddItem(Item i, [CanBeNull] Character other)
        {
            if (other != null)
                _ = other.Inventory.RemoveItem(i);

            var index = IndexOfItem(i);
            if (index != null)
            {
                Items[(int)index].quantity++;
                return i;
            }

            Items.Add(i);
            return i;
        }
        
        public Item RemoveItem(Item i)
        {
            Items.Remove(i);
            return i;
        }
    }
}
