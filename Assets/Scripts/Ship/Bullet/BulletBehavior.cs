using UnityEngine;

public class BulletBehavior : Bullet
{
    [Header("Settings")]
    public float velocity;
    public int maxTargets;

    [Header("Behaviour")]
    int currentTargets;
    ShipType originType;
    [HideInInspector] public int damage;


    void Start()
    {
        Invoke("DestroyBullet", 10);
    }


    void Update()
    {
        Movement();
    }


    void Movement()
    {
        gameObject.transform.Translate(velocity * Time.deltaTime * GameManager.Instance.gameTime, 0, 0);
    }


    public void SetBulletStats(int newDamage, ShipType myOriginType, float newSize = 0)
    {
        originType = myOriginType;
        damage = newDamage;
        if (newSize == 0)
            newSize = gameObject.transform.localScale.x;

        currentTargets = 0;
        gameObject.transform.localScale = new Vector3(newSize, newSize, newSize);
    }


    void OnTriggerEnter(Collider other)
    {
        if (currentTargets < maxTargets)
        {
            if (other.GetComponent<Status>())
            {
                if (other.GetComponent<Status>().myType != originType)
                {
                    other.GetComponent<Status>().TakeDamage(this.damage);
                    currentTargets++;
                    CheckDead();
                }
            }
            else if (other.GetComponent<ShieldBehavior>())
            {
                if (other.GetComponent<ShieldBehavior>().myShip.myType != originType)
                {
                    other.GetComponent<ShieldBehavior>().TakeDamage(this.damage);
                    currentTargets++;
                    CheckDead();
                }
            }else if(other.GetComponent<EnemysBehavior>() && originType == ShipType.HERO)
            {
                other.GetComponent<EnemysBehavior>().TakeDamage(this.damage);
                currentTargets++;
                CheckDead();
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Arena"))
        {
            if (originType == ShipType.HERO)
                DestroyBullet();
            else
                Destroy(this.gameObject);
        }
    }


    void CheckDead()
    {
        if (currentTargets >= maxTargets)
        {
            if (originType == ShipType.HERO)
                DestroyBullet();
            else
                Destroy(this.gameObject);
        }
    }
}
