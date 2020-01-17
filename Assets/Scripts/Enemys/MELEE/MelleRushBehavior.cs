using UnityEngine;
using System.Collections;

public class MelleRushBehavior : EnemysBehavior
{

    [SerializeField]
    int maxTargets;


    [SerializeField]
    float rushSpeed, rushDistance, rushIncrementSpeed;
    float  normalIncrementSpeed;
    Vector3 startRushPoint;

    bool rushOn;

    void Start()
    {
        StartStatus();
        shipTransform = GameObject.Find("AllShip").transform;

        normalIncrementSpeed = agent.acceleration;
    }


    void Update()
    {
        MoveMelle();
    }


    void MoveMelle()
    {

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(shipTransform.position.x, shipTransform.position.z)) < rushDistance && !rushOn)
        {
            Rush();
        }
        else if (Vector3.Distance(startRushPoint, this.transform.position) > rushDistance *1.6f && rushOn)
        {
            ReleaseRush();

        }


        if(agent.enabled)
        MoveToShip();
    }


    void Rush()
    {
        transform.LookAt(shipTransform);
        this.agent.speed = rushSpeed;
        this.agent.stoppingDistance = 0;
        this.agent.acceleration = rushIncrementSpeed;
        rushOn = true;

        startRushPoint = this.transform.position;
    }


    void ReleaseRush()
    {
        this.agent.Stop();
        this.agent.speed = status[level - 1].movimentSpeed ;
        this.agent.acceleration = normalIncrementSpeed;
        rushOn = false;

        agent.enabled = false;
        agent.enabled = true;
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
