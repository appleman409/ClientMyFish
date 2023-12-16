using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BuyFish : MonoBehaviour
{
    public static BuyFish instance;
    [Header("BuyMenu Manager")] 
    public GameObject BuyMenu;
    private int ItemID;
    private float Exp;
    private int Coin;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Buyed;
    public Image ImageFish;
    public Button Men;
    public Button Women;
    public int CountBuyed;
    public GameObject FishObject;
    private bool hasbuy = false;
    public GameObject fishbool;
    private float timer;

    void Awake()
    {
        instance = this;
        CountBuyed = 0;
    }

    void Update()
    {
        if (CountBuyed % 2 == 0)
        {
            Men.interactable = false;
            Women.interactable = true;
        }
        else
        {
            Men.interactable = true;
            Women.interactable = false;
        }
        Buyed.text = CountBuyed.ToString();
    }

    public void BuyStart(int ItemID, string name, int cost, float exp, int coin, float time)
    {
        BuyMenu.SetActive(true);
        CountBuyed = 0;
        this.ItemID = ItemID;
        Exp = exp;
        Coin = coin;
        ImageFish.sprite = Resources.Load<Sprite>($"Fish/{ItemID.ToString()}");
        Name.text = name;
        Cost.text = cost.ToString();
        Buyed.text = CountBuyed.ToString();
        hasbuy = true;
        timer = time;
    }

    public void Buy()
    {
        Player p = Player.instance;
        if (p.aquarium[p.SlotAqua].MaxFish > p.aquarium[p.SlotAqua].CurFish  && p.aquarium != null)
        {
            
            p.aquarium[p.SlotAqua].CurFish++;
            Debug.Log(p.aquarium[p.SlotAqua].fishs.Length);
            int curfish = p.aquarium[p.SlotAqua].CurFish;
            p.aquarium[p.SlotAqua].fishs[curfish] = new Player.Fish();
            Player.Fish fish = p.aquarium[p.SlotAqua].fishs[curfish];
            fish.FishId = ItemID;
            fish.Gender = (CountBuyed % 2);
            fish.Level = 1;
            fish.Grow = 0;
            fish.getCoin = Coin;
            fish.getExp = Exp;
            fish.TimeFood = 0;
            p.aquarium[p.SlotAqua].fishs[curfish] = fish;
            
            ClientSend.BuyFish(p.Uin, ItemID, Name.text,  fish.Gender, p.aquarium[p.SlotAqua].ID, Int32.Parse(Cost.text), Exp, Coin,timer);
            
            
          /*  float randomX = Random.Range(-8.6f, 7.6f);
            float randomY = Random.Range(-2.5f, 1f) ;
            GameObject go = Instantiate(FishObject, transform.position, Quaternion.identity) as GameObject;
            go.transform.parent = GameObject.Find("Fishs").transform;
            go.transform.position = new Vector3(randomX, randomY, (float)90.00);
            var Fishimage = go.GetComponent<SpriteRenderer>();
            Fishimage.sprite = ImageFish.sprite;
            go.transform.localScale = new Vector3(10, 10, 1);
            Fish.intance.getInfomation(ItemID, Exp, Coin, (CountBuyed%2));
            CountBuyed++;*/
        }
        
    }

    public void Cancel()
    {
        BuyMenu.SetActive(false);
        hasbuy = false;
    }

    public void spawnFish(int ID,int FishID, string name, int Level, float Food, float Grow, int g, int getCoin, float getExp, int IDAqua)
    {
        if (IDAqua != Player.instance.aquarium[Player.instance.SlotAqua].ID) return;
        float randomX = Random.Range(-8.6f, 7.6f);
        float randomY = Random.Range(-2.5f, 1f) ;
        GameObject go = Instantiate(FishObject, transform.position, Quaternion.identity) as GameObject;
        go.transform.SetParent(fishbool.transform);
        go.transform.position = new Vector3(randomX, randomY, (float)90.00);
        var Fishimage = go.GetComponent<SpriteRenderer>();
        Fishimage.sprite = Resources.Load<Sprite>($"Fish/{FishID.ToString()}");;
        go.transform.localScale = new Vector3(10, 10, 1);
        Fish.intance.setInfomation(ID ,FishID, name, getExp, getCoin, g, Food, Grow, Level);
        CountBuyed++;
    }
    
}
