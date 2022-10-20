using System.Collections.Generic;
using Characters.PlayerCharacter.InventorySystem.InventoryItem;
using Items;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters.PlayerCharacter.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private GameObject invItemPrefab;

        public void InvUpdate(List<Item> invRef)
        {
            int y = 200;
            foreach (var item in invRef)
            {
                var objRef = Instantiate(invItemPrefab, this.transform.GetChild(0));
                objRef.transform.localPosition = new Vector3(0, y, 0);
                y -= 80;

                objRef.GetComponent<InvItem>().Set(item);
            }
        }
    }
}
