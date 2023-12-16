using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{

    private string username;
    private string password;
    private string repassword;

    public void RegAccount()
    {
        if (username == null || password == null || repassword == null)
        {
            Debug.Log("Missing Somethings");
            MenuManager.instance.notifi("Vui lòng điền đủ thông tin!");
            return;
        }

        if (password != repassword)
        {
            Debug.Log("Password don't match");
            MenuManager.instance.notifi("Mật khẩu không trùng nhau!");
            return;
        }
        Debug.Log($"{username} {password}");
        ClientSend.RegisterReceived(username, password);
    }

    public void SetUser(string s)
    {
        username = s;
    }

    public void setpass(string p)
    {
        password = p;
    }

    public void setrpass(string rp)
    {
        repassword = rp;
    }
}
