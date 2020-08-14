using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [Header("Singleton")]
    public static SceneManager instance;
    public static SceneManager Instance { get { return instance; } }

    [Header("Behaviour")]
    private int cannon;
    private int drone;
    private void Awake()
    {
        if (SceneManager.Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += setCannonAndDrone;
    }

    public void goToTest(int cannon, int drone)
    {
        this.cannon = cannon;
        this.drone = drone;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void resetToHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void setCannonAndDrone(Scene scene, LoadSceneMode mode)
    {
        SecondaryCannonType chosenCannon = SecondaryCannonType.Angle0;
        DroneType chosenDrone= DroneType.Magnetic;
        if (scene.name=="Test")
        {
            switch (cannon)
            {
                case 0:
                    chosenCannon = SecondaryCannonType.Angle0;
                    break;
                case 1:
                    chosenCannon = SecondaryCannonType.Angle30;
                    break;
                case 2:
                    chosenCannon = SecondaryCannonType.Angle75;
                    break;
                case 3:
                    chosenCannon = SecondaryCannonType.Angle120;
                    break;

            }
            switch (drone)
            {
                case 0:
                    chosenDrone = DroneType.Attack;
                    break;
                case 1:
                    chosenDrone = DroneType.Magnetic;
                    break;
                case 2:
                    chosenDrone = DroneType.Healer;
                    break;
                case 3:
                    chosenDrone = DroneType.Rocket;
                    break;
            }
            FindObjectOfType<ShipController>().myDroneType=chosenDrone;
            FindObjectOfType<ShipController>().mySecondaryCannonType = chosenCannon;
        }
    }
}
