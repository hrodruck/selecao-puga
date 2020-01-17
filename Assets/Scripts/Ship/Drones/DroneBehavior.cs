using UnityEngine;

public class DroneBehavior : MonoBehaviour
{
    [Header("References")]
    public GameObject center;

    [Header("Settings")]
    public DroneType thisDroneType;
    public float orbitSpeed;


    protected void DroneMove()
    {
        this.center.transform.Rotate(new Vector3(0, orbitSpeed * Time.deltaTime * GameManager.Instance.gameTime, 0));
    }
}
