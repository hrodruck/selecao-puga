using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SavedCoins : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("coins"))
        {
            print("1");
            PlayerPrefs.SetInt("coins", 0);
        }
        else
        {
            print("2");
            gameObject.GetComponent<Text>().text= ""+ PlayerPrefs.GetInt("coins");
        }
    }
}
