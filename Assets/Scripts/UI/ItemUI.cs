using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public int id;
    public ItemInstance instanceItem;
    public Image artWork;
    public Text nameItem;
    public Text currentAmount;
    public Transform targetDisplay;

    public void SetValues()
    {
        SetArtWork();
        SetNameItem();
        CheckConsumableItem();                 
    }    

    public void SetArtWork()
    {
        artWork.sprite = instanceItem.item.artWorkItem;
    }

    public void SetNameItem()
    {
        nameItem.text = instanceItem.item.nameItem;
    }

    public void CheckConsumableItem()
    {
        if(instanceItem.item.consumableItem == true)
        {
            currentAmount.gameObject.SetActive(false);
        }
        else
        {
            currentAmount.text = instanceItem.amount.ToString();
        }
    }

    public void ClickOnItem()
    {
        HudController.InstanceHudController.DisplayItemsOption(this);
    }
}
