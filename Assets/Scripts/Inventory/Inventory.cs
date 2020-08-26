using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> listItens;
    
    public static Inventory instanceInventory;

    void Awake()
    {
        instanceInventory = this;
    }

    public void AddItem(Item item, int amountToAdd)
    {
        if(item.consumableItem)
        {
            ItemInstance itemInstance = new ItemInstance();
            itemInstance.item = item;
            itemInstance.amount = amountToAdd;

            listItens.Add(itemInstance);
        }
        else
        {
            bool hasItem = false;
            for(int index = 0; index < listItens.Count; index++)
            {
                if(listItens[index].item == item)
                {
                    if(listItens[index].amount < item.maxAmount)
                    {
                        int diffe = item.maxAmount - listItens[index].amount;
                        if(diffe >= amountToAdd)
                        {                           
                            listItens[index].amount += amountToAdd;
                            hasItem = true;                            
                        }
                        else
                        {
                            listItens[index].amount = item.maxAmount;
                            int rest = amountToAdd - diffe;
                            AddItem(item, rest);
                            hasItem = true;
                        }
                        break;
                    }
                }
            }
            if(!hasItem)
            {
                if(amountToAdd <= item.maxAmount)
                {
                    ItemInstance itemInstance = new ItemInstance();
                    itemInstance.item = item;
                    itemInstance.amount = amountToAdd;

                    listItens.Add(itemInstance);
                }
                else
                {
                    int rest = amountToAdd - item.maxAmount;
                    ItemInstance itemInstance = new ItemInstance();
                    itemInstance.item = item;
                    itemInstance.amount = item.maxAmount;

                    listItens.Add(itemInstance);
                    AddItem(item, rest);
                }
            }
        }        
    }

    public int GetItemCount(Item item)
    {
        int amountToReturn = 0;
        for(int index = 0; index < listItens.Count; index++)
        {
            if(listItens[index].item == item)
            {   
                amountToReturn += listItens[index].amount;
            }  
        }
        return amountToReturn;
    }

    public void RemoveItem(Item item, int amountToRemove)
    {
         for(int index = 0; index < listItens.Count; index++)
        {
            if(listItens[index].item == item)
            {   
                if(amountToRemove <= listItens[index].amount)
                {
                    listItens[index].amount -= amountToRemove;
                    if(listItens[index].amount == 0)
                    {
                        listItens.RemoveAt(index);
                        index--;
                        break;
                    }
                }
                else
                {
                    int diffe = amountToRemove - item.maxAmount;
                    listItens.RemoveAt(index);
                    index--;
                    RemoveItem(item, diffe);
                }
            }  
        }
    }
}
