using UnityEngine;
using System.Collections;

public class TowerRangeBehavior : EnemysBehavior
{

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float distanceToAttack;
    float currentDistanceToAttack;


    void Start()
    {
        StartStatus();
        CalculateFireRate();
        shipTransform = GameObject.Find("AllShip").transform;

    }


    void Update()
    {

        currentDistanceToAttack = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(shipTransform.position.x, shipTransform.position.z));

        if (currentDistanceToAttack < distanceToAttack)
        {
            transform.LookAt(shipTransform);
            AttackRange();
        }

    }

    void AttackRange()
    {
        if (currentBulletsToRecharg <= 0)
        {
            RechargMunition();
        }
        else if (currentFireDelayTime > fireDelayTime)
        {
            transform.LookAt(shipTransform);

            Shoot(bullet);
            currentBulletsToRecharg--;
            currentFireDelayTime = 0;
        }
        else
        {
            currentFireDelayTime += Time.deltaTime * GameManager.Instance.gameTime;
        }
    }


    void RechargMunition()
    {
        currentRechargTime += Time.deltaTime * GameManager.Instance.gameTime;

        if (currentRechargTime > status[level - 1].fireRechargTime)
        {
            currentBulletsToRecharg = status[level - 1].bulletsToRecharg;
            currentRechargTime = 0;
        }
    }


    void CalculateFireRate()
    {
        fireDelayTime = 1 / status[level - 1].fireRate;
    }

}
