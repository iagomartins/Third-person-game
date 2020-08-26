using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string nameItem;
    public Sprite artWorkItem;
    public int maxAmount;
    public int amountDamage;
    public float amountLife;
    public float amountStamina;
    public bool consumableItem;
    public ItemComponent itemComponent;
}
