using UnityEngine;
using System.Collections;

public class StaySafeBehavior : EnemysBehavior
 {
    [SerializeField]
    float minDistanceToFireAttack, maxDistanceToFireAttack, currentDistance;
    [SerializeField]
    GameObject bullet;


    void Start()
    {
        StartStatus();
        CalculateFireRate();
        agent.stoppingDistance = 1.5f;
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
        if (currentDistance > minDistanceToFireAttack-agent.stoppingDistance && currentDistance < maxDistanceToFireAttack+agent.stoppingDistance)
        {
            AttackRange();
        }
        else
        {
            Vector3 newLocal = shipTransform.position+(transform.position - shipTransform.position).normalized * (minDistanceToFireAttack * 0.2f + maxDistanceToFireAttack * 0.8f);
            newLocal.y = 1.25f;
            agent.SetDestination(newLocal);
            currentRechargTime = 0;
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