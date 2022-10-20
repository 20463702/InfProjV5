using System;
using Characters.PlayerCharacter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI.PickupUI
{
    public class ItemPickupUI : MonoBehaviour
    {
        private Button btnYes;
        private Button btnNo;
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
                btnYes = buttons[0];
                btnNo = buttons[1];
            }
            else
            {
                btnYes = buttons[1];
                btnNo = buttons[0];
            }
            
            btnNo.onClick.AddListener(() => { DestroyImmediate(this.gameObject); });
            btnYes.onClick.AddListener(PickUp);
        }

        private void PickUp()
        {
            PlayerRef.GiveItem(ItemRef);
            
            DestroyImmediate(this.gameObject);

            ItemRef.gameObject.SetActive(false);
        }
    }
}
