using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<ItemUI> listItemUI;
    public List<ItemUI> listActiveSlots;
    public ItemUI itemUI;
    public Transform gridItens;
    public int initialSlots;

    public void UpdateInventory()
    {
        if(listActiveSlots.Count != Inventory.instanceInventory.listItens.Count)
        {
            for(int index = 0; index < listActiveSlots.Count; index++)
            {
                listActiveSlots[index].gameObject.SetActive(false);
            }

            listActiveSlots.Clear();

            for(int index = 0; index < Inventory.instanceInventory.listItens.Count; index++)
            {
                ItemUI slot = TryGetAvaliableItemUI();
                slot.instanceItem = Inventory.instanceInventory.listItens[index];
                listActiveSlots.Add(slot);
            }
        }  

        for(int index = 0; index < listActiveSlots.Count; index++)
        {
            listActiveSlots[index].SetValues();
        }      
    }

    private ItemUI TryGetAvaliableItemUI()
    {
        ItemUI slot = null;
        
        for(int index = 0; index < listItemUI.Count; index++)
        {
            if(!listItemUI[index].gameObject.activeSelf)
            {
                slot = listItemUI[index];
                break;
            }
        }
        if(slot == null)
        {
            slot = CreateItemUI();            
        }
        slot.gameObject.SetActive(true);
        return slot;
    }

    public void SetupInventory()
    {
        for(int index = 0; index < initialSlots; index++)
        {
            CreateItemUI();
        }
    }

    private ItemUI CreateItemUI()
    {
        ItemUI slot = Instantiate(itemUI, gridItens);
        itemUI.id = listItemUI.Count;
        listItemUI.Add(slot);
        slot.gameObject.SetActive(false);
        return slot;
    }

}
