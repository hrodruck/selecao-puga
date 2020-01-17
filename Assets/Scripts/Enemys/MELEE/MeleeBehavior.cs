using UnityEngine;
using System.Collections;

public class MeleeBehavior : EnemysBehavior
{
    [SerializeField]
    float distanceToAttack, maxAttackDistance;
    [SerializeField]
    int maxTargets;
    [SerializeField]
    GameObject attackView;

    [SerializeField]
    float rushSpeed,rushDistance, rushDelay, rushTime, rushIncrementSpeed;
    float currentRushDelay,currentRushTime, normalIncrementSpeed;

    bool rushOn;

    void Start()
    {
        StartStatus();
        shipTransform = GameObject.Find("AllShip").transform;
        agent.stoppingDistance = distanceToAttack - 0.2f;
        currentRushDelay = rushDelay;
        normalIncrementSpeed = agent.acceleration;
    }


    void Update()
    {
        MoveMelle();
    }


    void MoveMelle()
    {
        currentMeleeAttackTime += Time.deltaTime * GameManager.Instance.gameTime;
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(shipTransform.position.x, shipTransform.position.z)) < rushDistance && currentRushDelay > rushDelay)
        {
            Rush();
        }
        else
        {
            currentRushDelay += Time.deltaTime * GameManager.Instance.gameTime;
            currentRushTime += Time.deltaTime * GameManager.Instance.gameTime;
            if (currentRushTime > rushTime)
            {
                ReleaseRush();
            }
        }

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(shipTransform.position.x, shipTransform.position.z)) <= distanceToAttack && !rushOn)
        {
            if (currentMeleeAttackTime >= status[level - 1].meleeAttackTime)
            {
                AttackMelle();
            }
        }
        else
        {
            MoveToShip();
        }
    }


    void AttackMelle()
    {

        transform.LookAt(shipTransform);

        Ray ray = new Ray(this.gameObject.transform.position, Vector3.Normalize(-transform.position + shipTransform.position));
        RaycastHit[] hit = Physics.RaycastAll(ray, maxAttackDistance);

        for (int i = 0; i < hit.Length && i < maxTargets; i++)
        {

            if (hit[i].collider.GetComponent<ShieldBehavior>())
            {
                ShieldBehavior shield = hit[i].collider.GetComponent<ShieldBehavior>();
                shield.TakeDamage(status[level - 1].meleeDamage);
            }
            else if (hit[i].collider.GetComponent<ShipController>())
            {
                ShipController ship = hit[i].collider.GetComponent<ShipController>();
                ship.TakeDamage(status[level - 1].meleeDamage);
            }
        }

        currentMeleeAttackTime = 0;

        Shoot(attackView);
    }


    void Rush()
    {
        this.agent.speed = rushSpeed;
        this.agent.stoppingDistance = 0;
        this.agent.acceleration = rushIncrementSpeed;
        rushOn = true;
        currentRushDelay = 0;
    }


    void ReleaseRush()
    {
        this.agent.speed = status[level - 1].movimentSpeed;
        this.agent.stoppingDistance = distanceToAttack - 0.2f;
        this.agent.acceleration = normalIncrementSpeed;
        rushOn = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (rushOn)
        {
            if (other.GetComponent<ShipController>())
            {
                other.GetComponent<ShipController>().TakeDamage(status[level - 1].meleeDamage);
                ReleaseRush();
            }
            else if (other.GetComponent<ShieldBehavior>())
            {
                other.GetComponent<ShieldBehavior>().TakeDamage(status[level - 1].meleeDamage);
                ReleaseRush();
            }
        }
    }
}
