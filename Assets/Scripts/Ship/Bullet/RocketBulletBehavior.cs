using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RocketBulletBehavior : Bullet
{
    [Header("Settings")]
    public float velocity;
    public float indexToMultiplyDamage;

    [Header("Behaviour")]
    int currentTargets;
    ShipType originType;
    [HideInInspector] public float damage;
    [HideInInspector] public float radiusToDamage;


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


    public void SetBulletStats(float newDamage, ShipType myOriginType, float newRadiusToDamage, float newSize = 0)
    {
        originType = myOriginType;
        damage = newDamage * indexToMultiplyDamage;
        radiusToDamage = newRadiusToDamage;

        if (newSize == 0)
            newSize = gameObject.transform.localScale.x;

        currentTargets = 0;
        gameObject.transform.localScale = new Vector3(newSize, newSize, newSize);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Status>())
        {
            if (other.GetComponent<Status>().myType != originType)
            {
                CheckAllTargets();
            }
        }
        else if (other.GetComponent<ShieldBehavior>())
        {
            if (other.GetComponent<ShieldBehavior>().myShip.myType != originType)
            {
                CheckAllTargets();
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Arena"))
        {
            CheckAllTargets();
        }
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
                if ((newTargets[i].GetComponent<Status>() && newTargets[i].GetComponent<Status>().myType != originType) || (newTargets[i].GetComponent<ShieldBehavior>() && newTargets[i].GetComponent<ShieldBehavior>().myShip.myType != originType))
                {
                    if (!targetsAvaliable.Contains(newTargets[i].gameObject))
                        targetsAvaliable.Add(newTargets[i].gameObject);
                }
            }

            ApplyTotalDamage((GameObject[])targetsAvaliable.ToArray());
        }
    }


    void ApplyTotalDamage(GameObject[] targets)
    {
        foreach (GameObject ob in targets)
        {
            ob.GetComponent<Status>().TakeDamage(this.damage);
        }

        DestroyBullet();
    }
}
