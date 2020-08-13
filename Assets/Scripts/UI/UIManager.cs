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

    [Header("Behaviour")]
    private const int maxHealth = 5;
    private GameObject[] healthArray = new GameObject[maxHealth];
    private int currentHealth=0;
    private int currentCoins;
    private int playerHealthLevel;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        playerHealthLevel= playerShip.GetComponent<ShipController>().healthLevel;
        currentHealth = playerShip.GetComponent<ShipController>().allStatus[playerHealthLevel - 1].health;
        currentCoins = CurrencyManager.Instance.totalCurrencys;
        
    }
    private void Update()
    {
        currentHealth = playerShip.GetComponent<ShipController>().allStatus[playerHealthLevel - 1].health;
        currentCoins = CurrencyManager.Instance.totalCurrencys;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthText.text = "" + currentHealth;
        CoinsText.text = "" + currentCoins;

    }
}
