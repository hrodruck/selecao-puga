using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static UIManager Instance { get { return instance; } }


    [Header("References")]
    public GameObject playerShip;
    public Text healthText;
    public Text CoinsText;
    public Text timeText;
    public Text gameOverText;
    public Canvas canvas;

    [Header("Behaviour")]
    public int timeToWin = 10;
    private int currentHealth = 0;
    private int currentCoins;
    private int currentTimeLeft;
    private int playerHealthLevel;
    [HideInInspector] public float initialTime; // to be updated by game manager when the game is restarted

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        playerHealthLevel = playerShip.GetComponent<ShipController>().healthLevel;
        currentHealth = playerShip.GetComponent<ShipController>().allStatus[playerHealthLevel - 1].health;
        currentCoins = CurrencyManager.Instance.totalCurrencys;
        initialTime = Time.time;
    }
    private void Update()
    {
        currentHealth = playerShip.GetComponent<ShipController>().allStatus[playerHealthLevel - 1].health;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        currentCoins = CurrencyManager.Instance.totalCurrencys;
        currentTimeLeft = timeToWin - (int)(Time.time - initialTime);
        if (GameManager.Instance.endGame)
        {
            currentTimeLeft = timeToWin;
        }
        if (currentTimeLeft <= 0)
        {
            GameManager.Instance.EndGame(true);
        }
        healthText.text = "" + currentHealth;
        CoinsText.text = "" + currentCoins;
        timeText.text = "" + currentTimeLeft;
        if (GameManager.Instance.endGame)
        {
            canvas.GetComponent<Animator>().SetBool("endGame", true);
            if (GameManager.Instance.victory)
            {
                gameOverText.color = Color.green;
                gameOverText.text = "You won! Press Cancel (ESC) to go again!";
            }
            else
            {
                gameOverText.color = Color.red;
                gameOverText.text = "You lost! Press Cancel (ESC) to try again...";
            }
        }else
        {
            canvas.GetComponent<Animator>().SetBool("endGame", false);
            gameOverText.text = "";
        }
    }

    public void resetUI()
    {
        canvas.GetComponent<Animator>().SetBool("endGame", false);
        initialTime = Time.time;
    }
}
