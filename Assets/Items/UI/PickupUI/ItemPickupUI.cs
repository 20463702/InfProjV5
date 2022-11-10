using System;
using Characters;
using Characters.PlayerCharacter;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI.PickupUI
{
    public class ItemPickupUI : MonoBehaviour
    {
        private Button _btnYes;
        private Button _btnNo;
        [field: NonSerialized]
        public Item ItemRef { get; private set; }
        public void SetRefs(Item itemRef)
        {
            ItemRef = itemRef;
        }

        private void Start()
        {
            var buttons = GetComponentsInChildren<Button>();

            _btnYes = buttons[0];
            _btnNo = buttons[1];
            
            _btnNo.onClick.AddListener(() => { DestroyImmediate(gameObject); });
            _btnYes.onClick.AddListener(PickUp);
        }

        private void PickUp()
        {
            PlayerCharacter.PlayerRef.Inventory.AddItem(ItemRef, null);
            PlayerCharacter.PlayerRef.Inventory.InvUpdate();
           
            DestroyImmediate(gameObject);
            ItemRef.gameObject.SetActive(false);
        }
    }
}
