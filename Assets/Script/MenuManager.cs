using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [Header("Menu GameObject")] 
    public GameObject loadingMenu;
    public GameObject homeMenu;
    public GameObject registerMenu;
    public GameObject noti;
    public GameObject game;
    public GameObject regacc;
    public GameObject WaitGame;
    public TMP_Text textnoti;

    public Menu _menu;
    public enum Menu
    {
        Loading =1,
        Home,
        Register,
        noti,
        game,
        regacc,
        Waitgame
    }

    private void Awake()
    {
        instance = this;
        _menu = Menu.Loading;
    }

    void Update()
    {
        switch (_menu)
        {
            case Menu.Loading:
                loadingMenu.SetActive(true);
                homeMenu.SetActive(false);
                registerMenu.SetActive(false);
                game.SetActive(false);
                break;
            case Menu.Home:
                loadingMenu.SetActive(false);
                homeMenu.SetActive(true);
                registerMenu.SetActive(false);
                game.SetActive(false);
                break;
            case Menu.Register:
                loadingMenu.SetActive(false);
                homeMenu.SetActive(false);
                registerMenu.SetActive(true);
                game.SetActive(false);
                break;
            case Menu.game:
                WaitGame.SetActive(false);
                homeMenu.SetActive(false);
                registerMenu.SetActive(false);
                game.SetActive(true);
                regacc.SetActive(false);
                break;
            case Menu.regacc:
                regacc.SetActive(true);
                break;
            case Menu.Waitgame:
                WaitGame.SetActive(true);
                loadingMenu.SetActive(true);
                homeMenu.SetActive(false);
                registerMenu.SetActive(false);
                game.SetActive(false);
                break;
        }
    }

    public void notifi(string msg)
    {
        textnoti.text = msg;
        noti.SetActive(true);
    }
    
    public void ChangeMenu(int menu)
    {
        _menu = (Menu)menu;
    }

    public void WaitBonus(float bonus)
    {
        Slider LoadingBar = WaitGame.transform.Find("Slider").GetComponent<Slider>();

        float temp = LoadingBar.value;
        if ((bonus + temp*100) >= 100)
        {
            _menu = Menu.game;
            Starting();
            Player.instance.changeSlotAqua(1);
            loadingMenu.SetActive(false);

        }
        else if ((bonus + temp * 100) == 80)
        {
            _menu = Menu.game;
        }
        LoadingBar.value = (bonus + temp*100) / 100;
         
    }

    public void getFish()
    {
        Starting();
        Player.instance.changeSlotAqua(1);
        loadingMenu.SetActive(false);
    }
    
    IEnumerator Starting()
    {

        // Chờ 10 giây
        yield return new WaitForSeconds(10f);
        
    }
}
