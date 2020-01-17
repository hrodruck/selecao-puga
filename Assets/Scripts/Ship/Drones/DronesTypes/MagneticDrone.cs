using UnityEngine;
using System.Collections.Generic;

public class MagneticDrone : DroneBehavior
{
    [Header("References")]
    public ShipController myShip;

    [Header("Settings")]
    public List<MagneticDroneStatus> status;
    [Range(1, 3)] public int level = 1;

    [Header("Behaviour")]
    List<Collider> allCoins = new List<Collider>();


    void Update()
    {
        DroneMove();
    }


    void OnTriggerStay(Collider other)
    {
        if (!allCoins.Contains(other) && other.CompareTag("Money"))
        {
            other.GetComponent<CurrencyBehaviour>().SetAttractionComponent(myShip.gameObject, status[level - 1].speedAttraction);
            allCoins.Add(other);
        }
    }
}

[System.Serializable]
public class MagneticDroneStatus
{
    public float speedAttraction;
}
