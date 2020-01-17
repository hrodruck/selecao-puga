using UnityEngine;
using System.Collections;

public class SuicideBehavior : EnemysBehavior
{
    void Start()
    {
        StartStatus();

        shipTransform = GameObject.Find("AllShip").transform;
    }


    void Update()
    {
        UpdateMovimenteSpeed();
        MoveToShip();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.transform.CompareTag("Ship")) 
        {
            Dead();
        }
    }
}
