using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
    public PlayerController player;
    public Weapon[] weapons;
    public WeaponController weaponController;
    public Slider sliderLife;
    public Slider sliderStamina;
    public Gradient gradientLife;
    public Image fillLife;
    public Image[] imageWeapons;
    public Text textAmmo;
    public Text textCurrentAmmo;
    public bool isInventory = false;
    public GameObject inventoryPanel;
    public InventoryUI inventoryUI;
    public GameObject displayItemOptionPanelCons;
    public GameObject displayItemOptionPanel;
    public ItemUI inspectedItem;

    public static HudController InstanceHudController;

    void Start()
    {
        SetMaxLife();
        SetMaxStamina();
        inventoryUI.SetupInventory();
        InstanceHudController = this;
    }
    void Update()
    {
        SetLife();
        SetStamina();
        player.RegenStamina();
        SetAmmo();
        SetCurrentAmmo();
        SetImageWeapon();
        InputsInvetory();
        inventoryUI.UpdateInventory();
    }

    public void SetLife()
    {
        sliderLife.value = player.currentLife;
        fillLife.color = gradientLife.Evaluate(sliderLife.normalizedValue);
    }
    public void SetMaxLife()
    {
        sliderLife.maxValue = player.maxLife;
        sliderLife.value = player.currentLife;
        fillLife.color = gradientLife.Evaluate(1f);
    }
    public void SetStamina()
    {
        sliderStamina.value = player.currentStamina;
    }
    public void SetMaxStamina()
    {
        sliderStamina.maxValue = player.maxStamina;
        sliderStamina.value = player.currentStamina;
    }

    public void SetAmmo()
    {
        textAmmo.text = weapons[weaponController.selectedWeapon].ammo.ToString();
    }
    public void SetCurrentAmmo()
    {
        textCurrentAmmo.text = weapons[weaponController.selectedWeapon].currentAmmo.ToString();
    }
    public void SetImageWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            imageWeapons[i].gameObject.SetActive(false);
        }
        imageWeapons[weaponController.selectedWeapon].gameObject.SetActive(true);
    }

    public void InputsInvetory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isInventory)
            {
                CloseInventory();
                displayItemOptionPanelCons.gameObject.SetActive(false);
                displayItemOptionPanel.gameObject.SetActive(false);
            }
            else
            {
                OpenInventory();
                displayItemOptionPanelCons.gameObject.SetActive(false);
                displayItemOptionPanel.gameObject.SetActive(false);
            }

        }
    }

    public void OpenInventory()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        isInventory = true;

        inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isInventory = false;

        inventoryPanel.SetActive(false);
    }

    public void DisplayItemsOption(ItemUI itemUI)
    {
        if (itemUI.instanceItem.item.consumableItem)
        {
            displayItemOptionPanelCons.transform.position = itemUI.targetDisplay.position;
            inspectedItem = itemUI;
            displayItemOptionPanelCons.SetActive(true);
            displayItemOptionPanel.SetActive(false);
        }
        else
        {
            displayItemOptionPanel.transform.position = itemUI.targetDisplay.position;
            inspectedItem = itemUI;
            displayItemOptionPanelCons.SetActive(false);
            displayItemOptionPanel.SetActive(true);
        }
    }

    public void CloseDisplayItemsOption()
    {
        displayItemOptionPanelCons.SetActive(false);
        displayItemOptionPanel.SetActive(false);
    }

    public void UseItem()
    {
        if (HudController.InstanceHudController.inventoryUI.itemUI.instanceItem.item.consumableItem)
        {
            if (PlayerController.instancePlayer.currentLife < 100)
            {
                switch (HudController.InstanceHudController.inventoryUI.itemUI.instanceItem.item.itemType)
                {
                    case ItemType.GREEN_HERB:
                        PlayerController.instancePlayer.AddLife(inspectedItem.instanceItem.item.amountLife);
                        displayItemOptionPanelCons.SetActive(false);
                        Inventory.instanceInventory.listItens.Remove(inspectedItem.instanceItem);
                        break;

                    case ItemType.RED_HERB:
                        PlayerController.instancePlayer.AddLife(inspectedItem.instanceItem.item.amountLife);
                        displayItemOptionPanelCons.SetActive(false);
                        Inventory.instanceInventory.listItens.Remove(inspectedItem.instanceItem);
                        break;
                }
            }
        }
    }

    public void DiscartItem()
    {
        Inventory.instanceInventory.listItens.Remove(inspectedItem.instanceItem);
        Vector3 positionPlayer = PlayerController.instancePlayer.transform.position + Vector3.up * 0.08f + PlayerController.instancePlayer.transform.forward;
        ItemComponent drop = GameObject.Instantiate(inspectedItem.instanceItem.item.itemComponent, positionPlayer, Quaternion.identity);
        drop.instanceItem.amount = inspectedItem.instanceItem.amount;
        displayItemOptionPanelCons.SetActive(false);
        displayItemOptionPanel.SetActive(false);
    }
}
