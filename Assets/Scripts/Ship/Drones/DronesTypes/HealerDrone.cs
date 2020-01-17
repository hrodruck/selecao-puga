using UnityEngine;
using System.Collections.Generic;

public class HealerDrone : DroneBehavior
{
    [Header("References")]
    public ShipController currentShip;

    [Header("Settings")]
    public List<HealerDroneStatus> status;
    [Range(1, 3)] public int level = 1;

    [Header("Behaviour")]
    int maxHealth;
    float currentTimeToRestore;


    void Start()
    {
        maxHealth = currentShip.allStatus[currentShip.healthLevel - 1].health;
        currentTimeToRestore = status[level - 1].delayTimeToRestore;
    }


    void Update()
    {
        DroneMove();

        currentTimeToRestore += Time.deltaTime * GameManager.Instance.gameTime;

        if (currentTimeToRestore > status[level - 1].delayTimeToRestore)
            RestoreHealth();
    }


    void RestoreHealth()
    {
        if (currentShip.allStatus[currentShip.healthLevel - 1].health + status[level - 1].healthValueToRestore <= maxHealth)
        {
            currentShip.allStatus[currentShip.healthLevel - 1].health += status[level - 1].healthValueToRestore;
            currentTimeToRestore = 0;
        }
    }
}

[System.Serializable]
public class HealerDroneStatus
{
    public int healthValueToRestore;
    public float delayTimeToRestore;
}
