using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text priceTxt;
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
        print("counter = " + counter);
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

    public enum ItemType {
        PotionUpgrade,
        Protection
    }

}