using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text priceTxt;
    [SerializeField] Image icon;
    ItemData itemData;
    public int counter = 1;
    Shop shop;
    int price;

    // Start is called before the first frame update
    void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    public void SetItemData(ItemData itemData){
        this.itemData = itemData;
        price = itemData.originalPrice;
        SetIcon(itemData.itemType);
        UpdateItemData();
    }

    void UpdateItemData(){
        itemName.text = itemData.itemName;
        priceTxt.text = price.ToString();
    }

    public void BuyItem(){
        if (shop.GetMoney() < price) return;
        shop.UpdateMoney(-price);
        counter++;
        UpdateItemPrice();
        // function
        shop.BuyItem(itemData.itemType);

    }

    public void UpdateItemPrice(){
        if (counter>itemData.stock) {
            GetComponent<Button>().interactable = false;
            priceTxt.text = "NO STOCK";
        } else {
            GetComponent<Button>().interactable = true;
            price = itemData.originalPrice * counter;
            priceTxt.text = price.ToString();
        }
        
    }

    public ItemData GetItemData(){
        return itemData;
    }

    void SetIcon(ItemType itemType) {
        ColorSelector cs = FindObjectOfType<ColorSelector>();
        switch (itemType)
        {
            case ItemType.Potion0Upgrade:
                icon.color = cs.potions[0].color;
            break;
            case ItemType.Potion1Upgrade:
                icon.color = cs.potions[1].color;
            break;
            case ItemType.Potion2Upgrade:
                icon.color = cs.potions[2].color;
            break;
            case ItemType.Protection:
            break;
        }
    }

    public enum ItemType {
        Potion0Upgrade,
        Potion1Upgrade,
        Potion2Upgrade,
        Protection
    }

}