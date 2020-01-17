using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public static CurrencyManager Instance { get { return instance; } }


    [Header("References")]
    public GameObject coinMesh;

    [Header("Settings")]
    public int maxCountForCurrencys;

    [Header("Behaviour")]
    [HideInInspector] public int totalCurrencys;


    void Awake()
    {
        instance = this;
    }


    public void AddCurrency(int valueToAdd)
    {
        totalCurrencys += valueToAdd;
    }
}
