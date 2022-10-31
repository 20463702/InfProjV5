using System.Collections.Generic;
using Characters.PlayerCharacter.InventorySystem.InventoryItem;
using Items;
using UnityEngine;

namespace Characters.Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        [SerializeField]
        protected GameObject invItemPrefab;
        protected GameObject _panel;

        private void Start()
        {
            _panel = transform.GetChild(0).gameObject;
        }

        public void InvUpdate(List<Item> invRef)
        {
            int y = 200;
            for (int i = 0, n = _panel.transform.childCount; i < n; i++)
                Destroy(_panel.transform.GetChild(i).gameObject);
            
            foreach (var item in invRef)
            {
                var objRef = Instantiate(invItemPrefab, transform.GetChild(0));
                objRef.GetComponent<InvItem>().Set(item);

                objRef.transform.localPosition = new Vector3(0, y, 0);
                y -= 80;
            }
        }
    }
}
