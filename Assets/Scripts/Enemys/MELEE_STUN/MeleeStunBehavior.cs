using UnityEngine;
using System.Collections;

public class MeleeStunBehavior : EnemysBehavior {


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

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(shipTransform.position.x, shipTransform.position.z)) <= distanceToAttack)
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
                ship.EnableStun(status[level -1].stunTime);
            }
        }

        currentMeleeAttackTime = 0;

        Shoot(attackView);
    }

}
