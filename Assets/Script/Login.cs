using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{
    private string username;
    private string password;
    
    public void SetUser(string s)
    {
        username = s;
    }

    public void setpass(string p)
    {
        password = p;
    }

    public void LogAccount()
    {
        if (username == null || password == null)
        {
            Debug.Log("Invaild Data");
            return;
        }
        ClientSend.LoginReceived(username, password);
    }
}
