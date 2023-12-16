using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class ItemShop : MonoBehaviour
{
    public static ItemShop instance; 
    
    [Header("ItemShop Infomation")] 
    public int ItemId;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Infomation;
    public TextMeshProUGUI Gold;
    public Image ImageFish;
    public float exp;
    private float timer;
    public int getcoin;

    void Start()
    {
        var Shop = new ShopManager.Shop();
        switch (gameObject.transform.parent.name)
        {
            case "Fish":
                Shop = ShopManager.instance.Shops[1];
                break;
            case "trangtri":
                Shop = ShopManager.instance.Shops[2];
                break;
            case "taphoa":
                Shop = ShopManager.instance.Shops[3];
                break;
            case "sukien":
                Shop = ShopManager.instance.Shops[4];
                break;
        }
        for (int i = 0; i < Shop.ItemShops.Length; i++)
        {
            var ItemShop = Shop.ItemShops[i];
            if (ItemShop != null)
            {
                Debug.Log("Item "+i);
                if (!ItemShop.hasStore)
                {
                    Debug.Log("hasstore" +i);
                    ItemId = ItemShop.ItemId;
                    exp = ItemShop.GetExp;
                    timer = ItemShop.Timer;
                    getcoin = ItemShop.GetGold;
                    Level.text = ItemShop.Level.ToString();
                    Name.text = ItemShop.Name;
                    Infomation.text =
                        $"Thời lượng: {ItemShop.Timer.ToString()}\nLợi nhuận: {ItemShop.GetGold.ToString()}\nExp: {ItemShop.GetExp.ToString()}";
                    Gold.text = ItemShop.Gold.ToString();
                    ImageFish.sprite = Resources.Load<Sprite>($"Fish/{ItemId.ToString()}");
                    ItemShop.hasStore = true;
                    Shop.ItemShops[i] = ItemShop;
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }

    public void Buy()
    {
        ShopManager.instance._menu = ShopManager.Menu.Stop;
        BuyFish.instance.BuyStart(ItemId, Name.text, int.Parse(Gold.text), exp, getcoin, timer);
    }
    
}
