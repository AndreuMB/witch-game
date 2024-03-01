using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    [SerializeField] List<ItemData> itemsData;
    [SerializeField] GameObject itemsContainer;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] TMP_Text moneyTxt;
    [SerializeField] int money;
    [SerializeField] GameObject shopCustomer;
    List<Item> items = new();
    // Start is called before the first frame update
    void Start()
    {
        foreach (ItemData itemData in itemsData)
        {
            GameObject item = Instantiate(itemPrefab, itemsContainer.transform);
            item.GetComponent<Item>().SetItemData(itemData);
            items.Add(item.GetComponent<Item>());
        }
    }

    public void ToggleShop(){
        shopPanel.SetActive(!shopPanel.activeInHierarchy);
    }

    public void UpdateMoney(int money){
        this.money += money;
        moneyTxt.text = this.money.ToString();
    }

    public int GetMoney(){
        return money;
    }

    public void BuyItem(Item.ItemType itemType){
        shopCustomer.GetComponent<IShop>().BoughtItem(itemType);
    }

    public void RestockConsumables(){
        print("enter restock consumables");
        foreach (Item item in items)
        {
            ItemData itemData = item.GetItemData();
            if (!itemData.consumable) continue;
            item.counter = 1;
            item.UpdateItemPrice();
        }
    }
}
