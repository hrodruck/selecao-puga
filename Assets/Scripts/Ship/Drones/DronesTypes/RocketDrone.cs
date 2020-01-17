using UnityEngine;
using System.Collections.Generic;

public class RocketDrone : DroneBehavior
{
    [Header("References")]
    public ShipController myShip;
    public GameObject cannonLocalSpawn;

    [Header("Settings")]
    public BulletType bullet;
    public List<RocketDroneStatus> status;
    [Range(1, 3)] public int level = 1;

    [Header("Behaviour")]
    GameObject target;
    List<GameObject> enemys;
    float TimeToShoot;
    float currentTimeToShoot;


    void Start()
    {
        SetShootRate();
        currentTimeToShoot = TimeToShoot;
    }


    void Update()
    {
        currentTimeToShoot += Time.deltaTime * GameManager.Instance.gameTime;
        UpdateTarget();
        DroneMove();
    }


    void UpdateTarget()
    {

        if (enemys.Count > 0)
        {
            List<GameObject> EnemysToRemove = new List<GameObject>();

            for (int i = 0; i < enemys.Count; i++)
            {
                if (enemys[i] == null)
                {
                    EnemysToRemove.Add(enemys[i]);
                }
            }

            for (int i = 0; i < EnemysToRemove.Count; i++)
            {
                enemys.Remove(EnemysToRemove[i]);
            }


            if (enemys.Count > 0)
            {
                CheckEnemyForDistance();
                transform.LookAt(target.transform.position);
                Shoot();
            }
        }
        else
        {
            transform.LookAt(myShip.targetToShoot);
        }
    }


    void CheckEnemyForDistance()
    {
        float currentMaxDistance = 10000;

        for (int i = 0; i < enemys.Count; i++)
        {
            if (Vector3.Distance(transform.position, enemys[i].transform.position) < currentMaxDistance)
            {
                target = enemys[i];
                currentMaxDistance = Vector3.Distance(transform.position, enemys[i].transform.position);
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            enemys.Add(other.gameObject);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy") && enemys.Contains(other.gameObject))
        {
            enemys.Remove(other.gameObject);
        }
    }


    void SetShootRate()
    {
        TimeToShoot = 1 / status[level - 1].shootForSeconds;
    }


    void Shoot()
    {
        if (currentTimeToShoot > TimeToShoot)
        {
            GameObject currentBullet = InstanceManager.Instance.InstanceBullet(bullet, cannonLocalSpawn.transform.position, cannonLocalSpawn.transform.rotation);
            currentBullet.GetComponent<RocketBulletBehavior>().SetBulletStats(status[level - 1].droneDamage, myShip.myType, status[level - 1].radiusLocalToDamage);

            currentTimeToShoot = 0;
        }
    }
}

[System.Serializable]
public class RocketDroneStatus 
{
    public float droneDamage;
    public float shootForSeconds;
    public float radiusLocalToDamage;
}
