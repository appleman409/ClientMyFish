using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Network.instance.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Network.instance.myid);
            _packet.Write("Welcome Back");
            Debug.Log("Send Welcome");
            SendTCPData(_packet);
        }
    }

    public static void RegisterReceived(string username, string passwork)
    {
        using (Packet _packet = new Packet((int)ClientPackets.registerReceived))
        {
            _packet.Write(Network.instance.myid);
            _packet.Write(username);
            _packet.Write(passwork);
            SendTCPData(_packet);
        }
    }
    
    public static void LoginReceived(string username, string passwork)
    {
        using (Packet _packet = new Packet((int)ClientPackets.loginReceived))
        {
            _packet.Write(Network.instance.myid);
            _packet.Write(username);
            _packet.Write(passwork);
            SendTCPData(_packet);
        }
    }
    public static void GetAccReceived(int Uin)
        {
            using (Packet _packet = new Packet((int)ClientPackets.getaccReceived))
            {
                _packet.Write(Network.instance.myid);
                _packet.Write(Uin);
                SendTCPData(_packet);
            }
        }

    public static void CreateAccReceived(string username, int Uin)
    {
        using (Packet _packet = new Packet((int)ClientPackets.createaccReceived))
        {
            _packet.Write(Network.instance.myid);
            _packet.Write(username);
            _packet.Write(Uin);
            SendTCPData(_packet);
        }
    }

    public static void LevelUpReceived(int Uin,int Level, float Exp)
    {
        using (Packet _packet = new Packet((int)ClientPackets.levelupReceived))
        {
            _packet.Write(Network.instance.myid);
            _packet.Write(Uin);
            _packet.Write(Level);
            _packet.Write(Exp);
            SendTCPData(_packet);
        }
    }

    public static void GainExpReceived(int Uin, float Exp)
    {
        
        using (Packet _packet = new Packet((int)ClientPackets.gainexpReceived))
        {
            _packet.Write(Network.instance.myid);
            _packet.Write(Uin);
            _packet.Write(Exp);
            SendTCPData(_packet);
        }
    }

    public static void GetShopReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.getshopReceived))
        {
            _packet.Write(Network.instance.myid);
            SendTCPData(_packet);
        }
    }

    public static void GetAquariumReceived(int Uin)
    {
        using (Packet _packet = new Packet((int)ClientPackets.getaquariumReceived))
        {
            _packet.Write(Network.instance.myid);
            _packet.Write(Uin);
            SendTCPData(_packet);
        }
    }

    public static void BuyFish(int Uin, int FishID, string name, int g, int IDAqua, int cost, float exp, int gold, float timer)
    {
        using (Packet _packet = new Packet((int)ClientPackets.buyfish))
        {
            _packet.Write(Network.instance.myid);
            _packet.Write(Uin);
            _packet.Write(FishID);
            _packet.Write(name);
            _packet.Write(g);
            _packet.Write(IDAqua);
            _packet.Write(cost);
            _packet.Write(exp);
            _packet.Write(gold);
            _packet.Write(timer);
            SendTCPData(_packet);
        }
    }

    public static void Eatting(int ID, float Food)
    {
        using (Packet _packet = new Packet((int)ClientPackets.eatting))
        {
            _packet.Write(Network.instance.myid);
            _packet.Write(ID);
            _packet.Write(Food);
            SendTCPData(_packet);
        }
    }
    
    #endregion
}
