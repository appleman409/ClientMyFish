using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    
    [Header("Shop Manager")]
    public GameObject MenuShop;
    public TextMeshProUGUI GoldTxt;
    public GameObject ItemObject;
    [Header("Shop Menu")] 
    public GameObject FishMenu;
    public GameObject trangtriMenu;
    public GameObject taphoaMenu;
    public GameObject sukienMenu;
    public Button Shop1;
    public Button Shop2;
    public Button Shop3;
    public Button Shop4;

    public Menu _menu;
    
    public enum Menu
    {
        Fish = 1,
        trangtri,
        taphoa,
        sukien,
        Start,
        Stop
    }

    public class Shop
    {
        public ItemShop[] ItemShops = new ItemShop[101];
    }
    
    public class ItemShop
    {
        public bool hasStore;
        public int ItemId;
        public string Name;
        public int Level;
        public int Gold;
        public float Timer;
        public int GetGold;
        public float GetExp;
    }

    public int fish=0, trangtri=0, taphoa=0, sukien=0;

    public Shop[] Shops = new Shop[5];

    void Start()
    {
        instance = this;
        for (int i = 1; i <= 4; i++)
        {
            Shops[i] = new Shop();
            for (int j = 0; j < 100; j++)
            {
                Shops[i].ItemShops[j] = new ItemShop();
            }
        }
    }

    public void GameStart()
    {
        _menu = Menu.Start;
        for (int i = 1; i <= 4; i++)
        {
            foreach (var j in Shops[i].ItemShops)
            {
                if (j != null && j.ItemId > 0)
                {
                    GameObject go;
                    switch (i)
                    {
                        case 1:
                            go = Instantiate(ItemObject, new Vector3 (0,0,0), Quaternion.identity) as GameObject;
                            go.transform.SetParent(FishMenu.transform);
                            go.transform.localScale = new Vector3(1, 1, 1);
                            break;
                        case 2:
                            go = Instantiate(ItemObject, new Vector3 (0,0,0), Quaternion.identity) as GameObject;
                            go.transform.SetParent(trangtriMenu.transform);
                            go.transform.localScale = new Vector3(1, 1, 1);
                            break;
                        case 3:
                            go = Instantiate(ItemObject, new Vector3 (0,0,0), Quaternion.identity) as GameObject;
                            go.transform.SetParent(taphoaMenu.transform);
                            go.transform.localScale = new Vector3(1, 1, 1);
                            break;
                        case 4:
                            go = Instantiate(ItemObject, new Vector3 (0,0,0), Quaternion.identity) as GameObject;
                            go.transform.SetParent(sukienMenu.transform); 
                            go.transform.localScale = new Vector3(1, 1, 1);
                            break;
                    }
                }   
            }
        }
        _menu = Menu.Stop;
    }

    void Update()
    {
        switch (_menu)
        {
            case Menu.Start:
                MenuShop.SetActive(true);
                FishMenu.SetActive(true);
                sukienMenu.SetActive(true);
                trangtriMenu.SetActive(true);
                taphoaMenu.SetActive(true);
                break;
            case Menu.Stop:
                MenuShop.SetActive(false);
                FishMenu.SetActive(false);
                sukienMenu.SetActive(false);
                trangtriMenu.SetActive(false);
                taphoaMenu.SetActive(false);
                break;
            case Menu.Fish:
                Shop1.enabled = false;
                Shop2.enabled = true;
                Shop3.enabled = true;
                Shop4.enabled = false;
                FishMenu.SetActive(true);
                sukienMenu.SetActive(false);
                trangtriMenu.SetActive(false);
                taphoaMenu.SetActive(false);
                break;
        }
    }

    public void ButtonShop()
    {
        if (!MenuShop.activeSelf)
        {
            MenuShop.SetActive(true);
            GoldTxt.text = Player.instance.Gold.ToString();
            _menu = Menu.Fish;

        }else {
            MenuShop.SetActive(false);
        }
    }

    public void Exit()
    {
        _menu = Menu.Stop;
    }

}
