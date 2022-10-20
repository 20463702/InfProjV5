using Items;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.PlayerCharacter.InventorySystem.InventoryItem
{
    public class InvItem : MonoBehaviour
    {
        private Item _itemRef;
        
        public void Set(Item i)
        {
            this._itemRef = i;
            
#if UNITY_EDITOR
            Debug.Log(this._itemRef);
            Debug.Log(this.transform.GetChild(0).gameObject);
            Debug.Log(this.transform.GetChild(1).gameObject);
#endif
            
            this.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = _itemRef.name;
        }
    }
}
