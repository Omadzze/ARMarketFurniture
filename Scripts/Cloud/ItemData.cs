using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public string itemName;

    public int itemPrice;

    public string itemDescription;

    public string itemColor;

    public int itemQuantity;

    public ItemData()
    {
    }

    public ItemData(string name, string color, int quantity, int price)
    {
        this.itemName = name;
        this.itemPrice = price;
        this.itemColor = color;
        this.itemQuantity = quantity;
    }
}
