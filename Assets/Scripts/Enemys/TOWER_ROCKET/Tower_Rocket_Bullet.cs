using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower_Rocket_Bullet : MonoBehaviour
{

    int damage, currentTargets;
    float radiusToDamage, velocity;


    void Start()
    {
        Destroy(this, 10);
    }


    void Update()
    {
        Movement();
    }


    void Movement()
    {
        gameObject.transform.Translate(velocity * Time.deltaTime * GameManager.Instance.gameTime, 0, 0);
    }


    public void SetTowerRocketBulletStatus(int newDamage, float newRadiusToApllyDamage, float bulletSpeed)
    {
        damage = newDamage;
        radiusToDamage = newRadiusToApllyDamage;
        velocity = bulletSpeed;
        currentTargets = 0;
    }


    void CheckAllTargets()
    {
        if (currentTargets < 1)
        {
            currentTargets++;

            Collider[] newTargets = Physics.OverlapSphere(this.transform.position, radiusToDamage);
            List<GameObject> targetsAvaliable = new List<GameObject>();

            for (int i = 0; i < newTargets.Length; i++)
            {
                if (newTargets[i].GetComponent<ShieldBehavior>())
                {
                    ShieldBehavior shield = newTargets[i].GetComponent<ShieldBehavior>();
                    shield.TakeDamage(damage);
                }
                else if (newTargets[i].GetComponent<ShipController>())
                {
                    ShipController ship = newTargets[i].GetComponent<ShipController>();
                    ship.TakeDamage(damage);
                }
            }

        }

        Destroy(this.gameObject);
    }


    void ApplyTotalDamage(GameObject[] targets)
    {
        foreach (GameObject ob in targets)
        {
            ob.GetComponent<Status>().TakeDamage(this.damage);
        }

        Destroy(this.gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ShipController>())
        {
            CheckAllTargets();
        }
        else if (other.GetComponent<ShieldBehavior>())
        {
            CheckAllTargets();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Arena"))
        {
            CheckAllTargets();
        }
    }
}
