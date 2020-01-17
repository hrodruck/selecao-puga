using UnityEngine;
using System.Collections;

public class MeleeRangerBehavior : EnemysBehavior {

    bool inAttack;

    [SerializeField]
    float distanceToAttack, maxAttackDistance;
    [SerializeField]
    int maxTargets;
    [SerializeField]
    GameObject attackView;

    void Start()
    {
        StartStatus();
        shipTransform = GameObject.Find("AllShip").transform;
        agent.stoppingDistance = distanceToAttack - 0.2f;
    }


    void Update()
    {
        MoveMelle();
    }


    void MoveMelle()
    {
        currentMeleeAttackTime += Time.deltaTime * GameManager.Instance.gameTime;

        if (!inAttack)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(shipTransform.position.x, shipTransform.position.z)) <= distanceToAttack)
            {
                if (currentMeleeAttackTime >= status[level - 1].meleeAttackTime)
                {
                    inAttack = true;
                }
            }
            else
            {
                MoveToShip();
            }
        }
        else 
        {
            currentMelleAttackDelay += Time.deltaTime * GameManager.Instance.gameTime;
            if (currentMelleAttackDelay > status[level - 1].meleeAttackDelay) 
            {
                AttackMeleeRange();
            }
        }
    }


    void AttackMeleeRange()
    {
        transform.LookAt(shipTransform);

        RaycastHit[] hit = Physics.BoxCastAll(this.transform.position, new Vector3(1, 1, 1), Vector3.Normalize(-transform.position + shipTransform.position),Quaternion.identity,maxAttackDistance);

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
        currentMelleAttackDelay = 0;
        inAttack =false;
        Shoot(attackView);
    }
}
