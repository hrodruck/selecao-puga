using UnityEngine;
using System.Collections;

public class RangeBehavior : EnemysBehavior
{
    [SerializeField]
    float minDistanceToFireAttack, maxDistanceToFireAttack, currentDistance;
    [SerializeField]
    GameObject bullet;


    void Start()
    {
        StartStatus();
        CalculateFireRate();
        agent.stoppingDistance = (minDistanceToFireAttack + maxDistanceToFireAttack) / 2; 
        shipTransform = GameObject.Find("AllShip").transform;
    }


    void Update()
    {
        UpdateMovimenteSpeed();
        MoveRange();
    }


    void MoveRange()
    {
        currentDistance = Vector2.Distance(new Vector2(shipTransform.position.x, shipTransform.position.z), new Vector2(this.transform.position.x, this.transform.position.z));
        if (currentDistance > maxDistanceToFireAttack)
        {
            agent.stoppingDistance = (minDistanceToFireAttack + maxDistanceToFireAttack) / 2;
            MoveToShip();
            currentRechargTime = 0;
        }
        else if (currentDistance < minDistanceToFireAttack)
        {
            Vector3 newLocal = transform.position - shipTransform.position;
            newLocal.y = 1.25f;
            agent.stoppingDistance = 0;
            agent.SetDestination(newLocal);
            currentRechargTime = 0;
        }
        else
        {
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
