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
        private GameObject _panel;
        private PlayerCharacter _playerRef;

        private void Start()
        {
            _panel = transform.GetChild(0).gameObject;
            _playerRef = GameObject.Find("Player").ConvertTo<PlayerCharacter>();
        }

        public void InvUpdate(List<Item> invRef)
        {
            int y = 200;
            for (int i = 0, n = _panel.transform.childCount; i < n; i++)
                Destroy(_panel.transform.GetChild(i).gameObject);
            
            foreach (var item in invRef)
            {
                var objRef = Instantiate(invItemPrefab, transform.GetChild(0));
                objRef.GetComponent<InvItem>().Set(item, _playerRef);

                objRef.transform.localPosition = new Vector3(0, y, 0);
                y -= 80;
            }
        }
    }
}
