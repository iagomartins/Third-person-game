using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemComponent : MonoBehaviour
{
    public ItemInstance instanceItem;
    public bool isTriggered;

    void Update()
    {
        if(Input.GetKey(KeyCode.F) && isTriggered)
        {
            if(instanceItem.item.maxAmount > 0)
            {
                Inventory.instanceInventory.AddItem(instanceItem.item, instanceItem.amount);            
                Destroy(gameObject);                
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.tag == "Player")
        {
            isTriggered = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            if(isTriggered)
            {
                isTriggered = false;
            }            
        }
    }
}
