using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAcc : MonoBehaviour
{
    private string username;

    public void SetUser(string s)
    {
        username = s;
    }

    public void SendAcc()
    {
        if (username == null)
        {
            Debug.Log("Invaild Data");return;
            
        }else
        {
            ClientSend.CreateAccReceived(username, Player.instance.Uin);
        }
        
    }

}
