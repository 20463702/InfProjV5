using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.PlayerCharacter;
using Items.Inventory.InventoryItem;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Items.Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        [field: SerializeField]
        protected GameObject invItemPrefab;
        [field: SerializeField]
        protected GameObject itemMenuPrefab;
        public GameObject panel;
        [field: SerializeField]
        public List<Item> Items { get; protected set; } = new();

        protected void Start()
        {
            panel = transform.GetChild(0).gameObject;
            panel.gameObject.SetActive(false);

            transform.GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(CloseInventory);

            InvUpdate();
        }

        public void InvUpdate()
        {
            var y = 200;
            for (int i = 0, n = panel.transform.childCount; i < n; i++)
                if (panel.transform.GetChild(i).GetComponent<InvItem>() != null)
                    Destroy(panel.transform.GetChild(i).gameObject);

            foreach (var item in Items)
            {
                var objRef = Instantiate(invItemPrefab, transform.GetChild(0));
                objRef.GetComponent<InvItem>().Set(item, itemMenuPrefab);
                objRef.transform.localPosition = new Vector3(0, y, 0);
                y -= 80;
            }
        }
        
        /// <param name="i">Item</param>
        /// <returns>Index of the item or null if not in inventory.</returns>
        public int? IndexOfItem(Item i)
        {
            foreach (var item in Items.Where(item => item.id == i.id))
                return Items.IndexOf(item);
            return null;
        }

        /// <param name="i">Item to be added</param>
        /// <param name="o">NULLABLE 'Other': Character the item comes from
        ///     (item will be removed from this character's inventory).</param>
        /// <param name="q">Quantity</param>
        /// <returns>Originally argument item.</returns>
        public Item AddItem(Item i, [CanBeNull] InventorySystem o = null, byte? q = null)
        {
            if (o != null)
                _ = o.RemoveItem(i);

            var index = IndexOfItem(i);
            if (index != null)
            {
                if (q != null)
                    Items[(int)index].quantity += (byte)q;
                else
                    Items[(int)index].quantity += i.quantity;
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

        private void CloseInventory()
        {
            panel.SetActive(false);
            PlayerCharacter.PlayerRef.Inventory.panel.SetActive(false);
            PlayerCharacter.PlayerRef.hasExternalInventoryOpen = false;
        }
    }
}
