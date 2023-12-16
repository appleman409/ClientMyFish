using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();
        
        Debug.Log($"Message from server: {_msg}");
        Network.instance.myid = _myId;
        ClientSend.WelcomeReceived();
        MenuManager.instance._menu = MenuManager.Menu.Home;
    }

    public static void Register(Packet _packet)
    {
        int result = _packet.ReadInt();
        if (result == 0)
        {
            Debug.Log("Tạo acc thành công");
            MenuManager.instance._menu = MenuManager.Menu.Home;

        }else if (result == 1)
        {
            Debug.Log("Đã trùng Username");
            MenuManager.instance.notifi("Đã trùng Username");
        }
    }

    public static void Login(Packet _packet)
    {
        int result = _packet.ReadInt();
        int Uin = _packet.ReadInt();
        if (result == 0)
        {
            Debug.Log("Đăng nhập thành công!!");
            ClientSend.GetAccReceived(Uin);
            Player.instance.Uin = Uin;
            Player.instance.connid = Network.instance.myid;
        }else if (result == 1)
        {
            Debug.Log("Sai Password");
            MenuManager.instance.notifi("Sai Mật Khẩu!");
        }
        else
        {
            Debug.Log("Không tìm thấy Username");
            MenuManager.instance.notifi("Không tìm thấy tên đăng nhập!");
        }
    }

    public static void Getacc(Packet _packet)
    {
        int result = _packet.ReadInt();
        int Uin,Level,Gold;
        float Exp, MaxExp;
        string Username;
        if (result == 0)
        {
            Uin = _packet.ReadInt();
            Username = _packet.ReadString();
            Level = _packet.ReadInt();
            Exp = _packet.ReadFloat();
            Gold = _packet.ReadInt();
            MaxExp = _packet.ReadFloat();
            Debug.Log($"{Uin} {Username} {Level} {Exp} {Gold}");
            Player.instance.Username = Username;
            Player.instance.Level = Level;
            Player.instance.Exp = Exp;
            Player.instance.Gold = Gold;
            Player.instance.MaxExp = MaxExp;
            MenuManager.instance._menu = MenuManager.Menu.Waitgame;
            ClientSend.GetShopReceived();
            Player.instance.SlotAqua = 1;
            
            
        }else if (result == 1)
        {
            MenuManager.instance._menu = MenuManager.Menu.regacc;
        }
    }

    public static void Createacc(Packet _packet)
    {
        int result = _packet.ReadInt();
        if (result == 0)
        {
            Debug.Log("Tạo acc thành công");
            MenuManager.instance._menu = MenuManager.Menu.game;
            Player.instance.Exp = 0;
            Player.instance.Gold = 1000;
            Player.instance.Level = 1;
            Player.instance.MaxExp = 100;
            Player.instance.SlotAqua = 1;
            Player.instance.aquarium[0].CurFish = 0;
            Player.instance.aquarium[0].Slot = 1;
            Player.instance.aquarium[0].MaxFish = 4;
        }
        else
        {
            Debug.Log("Đã trùng Username");
            MenuManager.instance.notifi("Username đã bị trùng");
        }
    }

    public static void LevelUp(Packet _packet)
    {
        Player.instance.Exp = _packet.ReadFloat();
        Player.instance.MaxExp = _packet.ReadFloat();
        Player.instance.Level = _packet.ReadInt();
    }

    public static void GainExp(Packet _packet)
    {
        Player.instance.Exp = _packet.ReadFloat();
        Infomation.instance.CheckExp();
    }

    public static void GetShop(Packet _packet)
    {
        int ItemShopCount = _packet.ReadInt();
        int fish = 0, trangtri = 0, taphoa = 0, sukien = 0;
        for (int i = 0; i < ItemShopCount; i++)
        {
            int type = _packet.ReadInt();
            var itemShop = new ShopManager.ItemShop();
            itemShop.ItemId = _packet.ReadInt();
            itemShop.Name = _packet.ReadString();
            itemShop.Level = _packet.ReadInt();
            itemShop.Timer = _packet.ReadFloat();
            itemShop.GetExp = _packet.ReadFloat();
            itemShop.GetGold = _packet.ReadInt();
            itemShop.Gold = _packet.ReadInt();
            switch (type)
            {
                case 1:
                    ShopManager.instance.Shops[type].ItemShops[fish] = itemShop;
                    fish++;
                    break;
                case 2:
                    ShopManager.instance.Shops[type].ItemShops[trangtri] = itemShop;
                    trangtri++;
                    break;
                case 3:
                    ShopManager.instance.Shops[type].ItemShops[taphoa] = itemShop;
                    taphoa++;
                    break;
                case 4:
                    ShopManager.instance.Shops[type].ItemShops[sukien] = itemShop;
                    sukien++;
                    break;
            }
        }
        ShopManager.instance.fish = fish;
        ShopManager.instance.trangtri = trangtri;
        ShopManager.instance.taphoa = taphoa;
        ShopManager.instance.sukien = sukien;
        ShopManager.instance.GameStart();
        
        MenuManager.instance.WaitBonus(20);
        ClientSend.GetAquariumReceived(Player.instance.Uin);
        
    }

    public static void GetAquarium(Packet _packet)
    {
        int numauqa = _packet.ReadInt();
        for (int i = 1; i <= numauqa; i++)
        {
            Player.instance.aquarium[i] = new Player.Aquarium();
            Player.instance.aquarium[i].ID = _packet.ReadInt();
            Player.instance.aquarium[i].Slot = _packet.ReadInt();
            Player.instance.aquarium[i].MaxFish = _packet.ReadInt();
            Player.instance.aquarium[i].CurFish = _packet.ReadInt();
            for (int j = 0; j < Player.instance.aquarium[i].CurFish; j++)
            {
                if (_packet.ReadInt() == Player.instance.aquarium[i].ID)
                {
                    Player.instance.aquarium[i].fishs[j] = new Player.Fish();
                    Player.instance.aquarium[i].fishs[j].ID = _packet.ReadInt();
                    Player.instance.aquarium[i].fishs[j].FishId = _packet.ReadInt();
                    Player.instance.aquarium[i].fishs[j].Name = _packet.ReadString();
                    Player.instance.aquarium[i].fishs[j].Level = _packet.ReadInt();
                    Player.instance.aquarium[i].fishs[j].TimeFood = _packet.ReadFloat();
                    Player.instance.aquarium[i].fishs[j].Grow = _packet.ReadFloat();
                    Player.instance.aquarium[i].fishs[j].Gender = _packet.ReadInt();
                    Player.instance.aquarium[i].fishs[j].getCoin = _packet.ReadInt();
                    Player.instance.aquarium[i].fishs[j].getExp = _packet.ReadFloat();
                    Debug.Log(Player.instance.aquarium[i].fishs[j].FishId + " " + i + " " + j);
                    //Player.Fish fish = Player.instance.aquarium[i].fishs[j];
                    //global::BuyFish.instance.spawnFish(fish.FishId, fish.Level, fish.TimeFood, fish.Grow, fish.Gender, fish.getCoin, fish.getExp, i);
                }
            }
        }
        MenuManager.instance.WaitBonus(20);
        Debug.Log("Aquairum");
        Infomation.instance.GameStart();
    }

    public static void BuyFish(Packet _packet)
    {
        int result = _packet.ReadInt();
        if (result == 1)
        {
            Player p = Player.instance;
            int curfish = p.aquarium[p.SlotAqua].CurFish;
            p.aquarium[p.SlotAqua].fishs[curfish] = new Player.Fish();
            Player.Fish fish = p.aquarium[p.SlotAqua].fishs[curfish];
            fish.ID = _packet.ReadInt();
            fish.FishId = _packet.ReadInt();
            fish.Name = _packet.ReadString();
            fish.Gender = _packet.ReadInt();
            fish.Level = _packet.ReadInt();
            fish.Grow = _packet.ReadFloat();
            fish.getCoin = _packet.ReadInt();
            fish.getExp = _packet.ReadFloat(); 
            
            fish.TimeFood = 0;
            p.aquarium[p.SlotAqua].fishs[curfish] = fish;
            global::BuyFish.instance.spawnFish(fish.ID ,fish.FishId, fish.Name, fish.Level, fish.TimeFood, fish.Grow, fish.Gender, fish.getCoin, fish.getExp, p.SlotAqua);
        }
    }

    public static void Eatting(Packet _packet)
    {
        
    }
    
}
