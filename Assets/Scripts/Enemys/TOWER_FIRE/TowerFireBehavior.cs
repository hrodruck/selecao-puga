using UnityEngine;
using System.Collections;

public class TowerFireBehavior : EnemysBehavior
{
    [SerializeField]
    float rotateSpeed, fireActiveTime;
    float currentFireActiveTime;

    [SerializeField]
    TowerFireAttack myFire;


    void Start()
    {
        StartStatus();
        shipTransform = GameObject.Find("AllShip").transform;
        myFire.damage = status[level - 1].fireDamage;
    }


    void Update()
    {
        RotateMove();

        if (currentFireActiveTime < fireActiveTime)
        {
            OnFire();
        }
        else 
        {
            Recharg();
        }

    }


    void RotateMove()
    {
        transform.Rotate(0,rotateSpeed * Time.deltaTime * GameManager.Instance.gameTime,0);
    }


    void OnFire()
    {
        currentFireActiveTime += Time.deltaTime * GameManager.Instance.gameTime;
        currentRechargTime = 0;

        if (!myFire.gameObject.activeInHierarchy)
        {
            myFire.gameObject.SetActive(true);
        }
    }


    void Recharg() 
    {
        currentRechargTime += Time.deltaTime * GameManager.Instance.gameTime;

        if (currentRechargTime > status[level - 1].fireRechargTime) 
        {
            currentFireActiveTime = 0;    
        }

        if (myFire.gameObject.activeInHierarchy)
        {
            myFire.gameObject.SetActive(false);
        }
    }

}
