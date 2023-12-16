using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public static Fish intance;

    [Header("Fish Infomation")] 
    [SerializeField]
    private int ID;
    private int FishId;
    private string Name;
    private int Level;
    private float Grow;
    private float getExp;
    private int getCoin;
    private int Gender;
    [SerializeField]
    private float TimeFood;
    public GameObject info;

    private void Awake()
    {
        intance = this;
    }

    public void getInfomation(int i,int itemid, float Exp, int Coin, int g)
    {
        ID = i;
        FishId = itemid;
        getExp = Exp;
        getCoin = Coin;
        Gender = g;
        TimeFood = 0;
        Grow = 0;
    }

    public void setInfomation(int i,int itemid, string name, float Exp, int Coin, int g, float food, float grow, int l)
    {
        ID = i;
        FishId = itemid;
        getExp = Exp;
        getCoin = Coin;
        Gender = g;
        TimeFood = food;
        Grow = grow;
        Level = l;
        Name = name;
    }

    public void showInfo()
    {
        if (!info.activeSelf)
        {
            info.SetActive(true);
        }
        else
        {
            info.SetActive(false);
        }
        
    }

    public float Food()
    {
        return TimeFood;
    }

    public void Food(float f)
    {
        TimeFood += f;
    }
    
}
