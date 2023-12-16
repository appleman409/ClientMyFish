using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Player Infomation")] 
    public int connid;
    public int Uin;
    public string Username;
    public float Exp;
    public float MaxExp;
    public int Gold;
    public int Level;
    [Header("Aquarium Infomation")] 
    public Aquarium[] aquarium = new Aquarium[5];
    public int SlotAqua;

    private void Awake()
    {
        instance = this;
    }

    public void changeSlotAqua(int saqua)
    {
        SlotAqua = saqua;
        Aquarium aquarium = Player.instance.aquarium[SlotAqua];
        Debug.Log(aquarium.CurFish);
        for (int j = 0; j < aquarium.CurFish; j++)
        {
            Player.Fish fish = Player.instance.aquarium[SlotAqua].fishs[j];
            global::BuyFish.instance.spawnFish(fish.ID,fish.FishId, fish.Name, fish.Level, fish.TimeFood, fish.Grow, fish.Gender, fish.getCoin, fish.getExp, SlotAqua);
        }
    }
    
    public class Fish
    {
        public int ID;
        public int FishId;
        public string Name;
        public int Level;
        public float Grow;
        public float getExp;
        public int getCoin;
        public int Gender;
        public float TimeFood;
    }

    public class Aquarium
    {
        public int ID;
        public int Slot;
        public int MaxFish;
        public int CurFish;
        public Fish[] fishs = new Fish[16];
}
}
