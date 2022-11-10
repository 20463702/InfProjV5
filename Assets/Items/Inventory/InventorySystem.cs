using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.PlayerCharacter;
using Items.Inventory.InventoryItem;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Items.Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        [SerializeField]
        protected GameObject invItemPrefab;
        [SerializeField]
        protected GameObject itemMenuPrefab;
        public GameObject Panel;
        [field: SerializeField]
        public List<Item> Items { get; protected set; } = new();

        protected void Start()
        {
            Panel = transform.GetChild(0).gameObject;
            Panel.gameObject.SetActive(false);

            transform.GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(CloseInventory);
        }

        public void InvUpdate()
        {
            int y = 200;
            for (int i = 0, n = Panel.transform.childCount; i < n; i++)
                if (Panel.transform.GetChild(i).GetComponent<InvItem>() != null)
                    Destroy(Panel.transform.GetChild(i).gameObject);

            foreach (var item in Items)
            {
                var objRef = Instantiate(invItemPrefab, transform.GetChild(0));
                objRef.GetComponent<InvItem>().Set(item, itemMenuPrefab);

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

        private void CloseInventory()
        {
            Panel.SetActive(false);
            PlayerCharacter.PlayerRef.Inventory.Panel.SetActive(false);
        }
    }
}
