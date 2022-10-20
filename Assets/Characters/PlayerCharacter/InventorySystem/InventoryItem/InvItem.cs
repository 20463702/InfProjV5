using Items;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.PlayerCharacter.InventorySystem.InventoryItem
{
    public class InvItem : MonoBehaviour
    {
        private Item itemRef;
        
        public void Set(Item i)
        {
            this.itemRef = i;
            
#if UNITY_EDITOR
            Debug.Log(this.itemRef);
            Debug.Log(this.transform.GetChild(0).gameObject);
            Debug.Log(this.transform.GetChild(1).gameObject);
#endif
            
            this.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = itemRef.name;
        }
    }
}
