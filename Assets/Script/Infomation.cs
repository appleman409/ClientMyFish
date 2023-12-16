using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Infomation : MonoBehaviour
{

    public static Infomation instance;
    
    public int MaxXP;

    private float lerpTimer;
    private float delayTimer;
    [Header("UI")] 
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Username;
    public TextMeshProUGUI Gold;
    public Image Avatar;

    public string AvatarLink = "https://avatarfiles.alphacoders.com/336/336678.png";

    private bool isGainExp = false;
    
    
    private void Awake()
    {
        instance = this;
    }
    public void GameStart()
    {
        frontXpBar.fillAmount = Player.instance.Exp / Player.instance.MaxExp;
        Level.text = Player.instance.Level.ToString();
        Username.text = Player.instance.Username;
        Gold.text = Player.instance.Gold.ToString();
        Debug.Log("Load Avatar");
        StartCoroutine(LoadImage(AvatarLink));
        Debug.Log("Load Done");
        
        MenuManager.instance.WaitBonus(60);
    }

    private void Update()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer > 5)
        {
            if (isGainExp)
            {
                CheckExp();
                delayTimer = 0;
            }
            
        }
        Gold.text = Player.instance.Gold.ToString();
    }

    public void GainExp(float Exp)
    {
        isGainExp = true;
        Player.instance.Exp += Exp;
        Player.instance.Exp = Mathf.Clamp(Exp, Player.instance.Exp, Player.instance.MaxExp);
        frontXpBar.fillAmount = Player.instance.Exp / Player.instance.MaxExp;
        Debug.Log($"{Player.instance.Exp} {Player.instance.MaxExp} {frontXpBar.fillAmount}");
        if(Player.instance.Exp >= Player.instance.MaxExp) LevelUp();
        else ClientSend.GainExpReceived(Player.instance.Uin, Player.instance.Exp);
        isGainExp = false;
    }

    public void LevelUp()
    {
        Player.instance.Exp -= Player.instance.MaxExp;
        Player.instance.Exp = Mathf.Clamp(Player.instance.Exp, 0, Player.instance.MaxExp);
        frontXpBar.fillAmount = Player.instance.Exp / Player.instance.MaxExp;
        Player.instance.Level++;
        Level.text = Player.instance.Level.ToString();
        ClientSend.LevelUpReceived(Player.instance.Uin, Player.instance.Level, Player.instance.Exp);
    }

    public void CheckExp()
    {
        if (Player.instance.Exp >= Player.instance.MaxExp)
        {
            LevelUp();
        }
        else
        {
            Player.instance.Exp = Mathf.Clamp(Player.instance.Exp, 0, Player.instance.MaxExp);
            frontXpBar.fillAmount = Player.instance.Exp / Player.instance.MaxExp;
        }
    }

    IEnumerator LoadImage(string link)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(link);
        yield return request.SendWebRequest();

       
        Texture2D mytexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        Sprite newSprite = Sprite.Create(mytexture, new Rect(0, 0, mytexture.width, mytexture.height),new Vector2(0.5f, 0.5f));

        Avatar.sprite = newSprite;
        
    }
}
