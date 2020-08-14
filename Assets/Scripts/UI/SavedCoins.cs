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
            PlayerPrefs.SetInt("coins", 0);
        }
        else
        {
            gameObject.GetComponent<Text>().text= ""+ PlayerPrefs.GetInt("coins");
        }
    }
}
