using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Net.Sockets;

public class Network : MonoBehaviour
{
    public static Network instance;

    [Header("Network Settings")] public string ServerIP = "127.0.0.1";
    public int ServerPort = 5500;
    public bool isConnected;

    public TcpClient PlayerScoket;
    public NetworkStream myStream;
    public StreamReader myReader;
    public StreamWriter MyWriter;
    public int myid;

    private Packet receivedData;

    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private byte[] asyncBuff;
    public bool shouldHandleData;
    
    
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitializedClientData();
        
        ConnectGameServer();
    }

    void ConnectGameServer()
    {
        if (PlayerScoket != null)
        {
            if (PlayerScoket.Connected || isConnected)
            {
                return;
            }
        }

        PlayerScoket = new TcpClient();
        PlayerScoket.ReceiveBufferSize = 4096;
        PlayerScoket.SendBufferSize = 4096;
        PlayerScoket.NoDelay = false;
        Array.Resize(ref asyncBuff, 8192);
        PlayerScoket.BeginConnect(ServerIP, ServerPort, new AsyncCallback(ConnectCallback), PlayerScoket);
        isConnected = true;
        
    }

    public void SendData(Packet _packet)
    {
        try
        {
            if (PlayerScoket != null)
            {
                myStream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
            }
        }
        catch (Exception _ex)
        {
            Debug.Log($"Error sending data to server via TCP: {_ex}");
        }
    }

    void ConnectCallback(IAsyncResult result)
    {
        if (PlayerScoket != null)
        {
            PlayerScoket.EndConnect(result);
            if (PlayerScoket.Connected == false)
            {
                isConnected = false;
                return;
            }
            else
            {
                PlayerScoket.NoDelay = true;
                myStream = PlayerScoket.GetStream();

                receivedData = new Packet();
                
                myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
            }

        }
    }

    void OnReceive(IAsyncResult result)
    {
        if (PlayerScoket != null)
        {
            int byteArray = myStream.EndRead(result);
            byte[] myBytes = null;
            Array.Resize(ref myBytes, byteArray);
            Buffer.BlockCopy(asyncBuff, 0, myBytes, 0, byteArray);

            if (byteArray == 0)
            {
                Debug.Log("You disconected Server");
                PlayerScoket.Close();
                return;
            }
            
            //HandleData
            receivedData.Reset(HandleData(myBytes));
            
            if (PlayerScoket == null) return;
            myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
        }
    }

    private bool HandleData(byte[] _data)
    {
        int _packetLenght = 0;

        receivedData.SetBytes(_data);
        if (receivedData.UnreadLength() >= 4)
        {
            _packetLenght = receivedData.ReadInt();
            if (_packetLenght <= 0)
            {
                return true;
            }
        }

        while (_packetLenght > 0 && _packetLenght <= receivedData.UnreadLength())
        {
            byte[] _packetBytes = receivedData.ReadBytes(_packetLenght);
            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_packetBytes))
                {
                    int _packetId = _packet.ReadInt();
                    packetHandlers[_packetId](_packet);
                }
            });
            _packetLenght = 0;
            if (receivedData.UnreadLength() >= 4)
            {
                _packetLenght = receivedData.ReadInt();
                if (_packetLenght <= 0)
                {
                    return true;
                }
            }
        }

        if (_packetLenght <= 1)
        {
            return true;
        }

        return false;
    }

    private void InitializedClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.welcome, ClientHandle.Welcome },
            { (int)ServerPackets.register, ClientHandle.Register},
            { (int)ServerPackets.login, ClientHandle.Login},
            { (int)ServerPackets.getacc, ClientHandle.Getacc},
            { (int)ServerPackets.createacc, ClientHandle.Createacc},
            { (int)ServerPackets.levelup, ClientHandle.LevelUp},
            { (int)ServerPackets.gainexp, ClientHandle.GainExp},
            { (int)ServerPackets.getshop, ClientHandle.GetShop},
            { (int)ServerPackets.getaquarium, ClientHandle.GetAquarium},
            { (int)ServerPackets.butfish, ClientHandle.BuyFish},
            { (int)ServerPackets.eatting, ClientHandle.Eatting}
        };
        Debug.Log("Intall Packet..");
    }
}
