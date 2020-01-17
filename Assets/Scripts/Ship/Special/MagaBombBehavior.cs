using UnityEngine;
using System.Collections.Generic;

public class MagaBombBehavior : MonoBehaviour
{

    [Header("References")]
    public Gizmo test;

    [Header("Settings")]
    public ShipType originType;
    public List<MegaBombStatus> status;
    [Range(1, 3)] public int megaBombLevel = 1;

    [Header("Behaviour")]
    SphereCollider myCollider;


    void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
    }


    void Update()
    {
        Expand();
        if (test != null)
            test.gizmoSizeAndRadius = myCollider.radius;
    }


    public void SetBombStatus(float newDamage, ShipType myOriginType)
    {
        status[megaBombLevel - 1].damage = newDamage;
        originType = myOriginType;
    }


    void Expand()
    {
        if (myCollider.radius < status[megaBombLevel - 1].maxRadius)
            myCollider.radius += status[megaBombLevel - 1].expandSpeed * Time.deltaTime * GameManager.Instance.gameTime;
        else
            Destroy(this.gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Status>())
        {
            if (other.GetComponent<Status>().myType != originType)
            {
                other.GetComponent<Status>().TakeDamage(this.status[megaBombLevel - 1].damage);
            }
        }
        else if (other.GetComponent<ShieldBehavior>())
        {
            if (other.GetComponent<ShieldBehavior>().myShip.myType != originType)
            {
                other.GetComponent<ShieldBehavior>().TakeDamage(this.status[megaBombLevel - 1].damage);
            }
        }
        else if (other.GetComponent<EnemysBehavior>() && originType == ShipType.HERO)
        {
            other.GetComponent<EnemysBehavior>().TakeDamage((int)this.status[megaBombLevel - 1].damage);
        }
    }
}

[System.Serializable]
public class MegaBombStatus
{
    public float durationRecharg;
    public int manaCost;

    public float expandSpeed;
    public float maxRadius;
    public float damage;
}