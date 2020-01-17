using UnityEngine;
using System.Collections;

public class TowerRocketBehavior : EnemysBehavior {

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float distanceToAttack,bulletSpeed,bulletRadiusToDamage;
    float currentDistanceToAttack;

	void Start () {

        StartStatus();
        CalculateFireRate();
        shipTransform = GameObject.Find("AllShip").transform;

	}
	

	void Update () {
        currentDistanceToAttack = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(shipTransform.position.x, shipTransform.position.z));

        if (currentDistanceToAttack < distanceToAttack)
        {
            AttackRocketTower();
        }
	}


    void AttackRocketTower()
    {
        transform.LookAt(shipTransform);
        if (currentFireDelayTime > fireDelayTime)
        {
            for (int i = 0; i < cannonsLocal.Length; i++)
            {
               GameObject currentBullet = (GameObject) Instantiate(bullet, cannonsLocal[i].position, cannonsLocal[i].rotation);
               currentBullet.GetComponent<Tower_Rocket_Bullet>().SetTowerRocketBulletStatus(status[level - 1].fireDamage, bulletRadiusToDamage, this.bulletSpeed);
            }

            currentFireDelayTime = 0;
        }
        else
        {
            currentFireDelayTime += Time.deltaTime * GameManager.Instance.gameTime;
        }
    }


    void CalculateFireRate()
    {
        fireDelayTime = 1 / status[level - 1].fireRate;
    }
}
