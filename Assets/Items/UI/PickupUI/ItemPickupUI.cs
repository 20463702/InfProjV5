using System;
using Characters.PlayerCharacter;
using Unity.VisualScripting;
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
        [field: NonSerialized]
        public PlayerCharacter PlayerRef { get; private set; }

        public void SetRefs(Item itemRef, PlayerCharacter playerRef)
        {
            this.ItemRef = itemRef;
            this.PlayerRef = playerRef;
        }

        private void Start()
        {
            var buttons = GetComponentsInChildren<Button>();

            if (buttons[0].name == "btnYes")
            {
                _btnYes = buttons[0];
                _btnNo = buttons[1];
            }
            else
            {
                _btnYes = buttons[1];
                _btnNo = buttons[0];
            }
            
            _btnNo.onClick.AddListener(() => { DestroyImmediate(this.gameObject); });
            _btnYes.onClick.AddListener(PickUp);
        }

        private void PickUp()
        {
            PlayerRef.GiveItem(ItemRef);
            
            DestroyImmediate(this.gameObject);

            ItemRef.gameObject.SetActive(false);
        }
    }
}
