using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public Image healthBar;

    private  byte maxHealth = 20;

    public static byte health;

    public static byte score;

    public Text scoreCounter;

    public Text scoreAfterDead;

    public GameObject deadScreen;

    public GameObject playerUI;

    public GameObject mainMenu;
    public GameObject startCam;
    public GameObject mainMenuCum;

    public static bool playMode = false;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = maxHealth;
        startCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        scoreCounter.text = ("Score: " + score);
        scoreAfterDead.text = ("Score: " + score);
        float hp = health / 20f;
        healthBar.fillAmount = hp;
        if (health == 0)
        {
            deadScreen.SetActive(true);
            playerUI.SetActive(false);
            mainMenu.SetActive(false);
        }
    }

    public void b_restart()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("MaxScore",score);
        score = 0;
    }

    public void b_exit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void b_play()
    {
        mainMenuCum.SetActive(false);
        startCam.SetActive(true);
        mainMenu.SetActive(false);
        playerUI.SetActive(true);
        playMode = true;
    }
}
