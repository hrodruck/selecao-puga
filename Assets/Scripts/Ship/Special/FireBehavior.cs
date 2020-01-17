using UnityEngine;
using System.Collections.Generic;

public class FireBehavior : MonoBehaviour
{
    [Header("References")]
    public Transform cannonTransform;

    [Header("Settings")]
    public ShipType myShipType;
    public List<FireStatus> status;
    [Range(1, 3)] public int fireLevel;

    [Header("Behaviour")]
    bool isAble = false;
    [HideInInspector] public float currentDuration;
    float maxAngleRotatePlus;
    float maxAngleRotateNegative;
    bool rotatePlusExis;


    void Awake()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cannonTransform.eulerAngles.y - 180, transform.eulerAngles.z);
        maxAngleRotatePlus = transform.eulerAngles.y + status[fireLevel - 1].maxAngleToRotate;
        maxAngleRotateNegative = transform.eulerAngles.y - status[fireLevel - 1].maxAngleToRotate;
        GetComponent<ParticleSystem>().Play();
    }


    void Update()
    {
        currentDuration += Time.deltaTime * GameManager.Instance.gameTime;
        if (currentDuration > status[fireLevel - 1].duration)
        {
            GetComponent<ParticleSystem>().Stop();
            this.gameObject.SetActive(false);
        }

        Move();
    }


    void Move()
    {
        if (!rotatePlusExis)
        {
            transform.Rotate(new Vector3(0, 0, -status[fireLevel - 1].speedMovement * Time.deltaTime * GameManager.Instance.gameTime));
            if (transform.localEulerAngles.y < maxAngleRotateNegative - 90)
            {
                rotatePlusExis = true;
            }
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, status[fireLevel - 1].speedMovement * Time.deltaTime * GameManager.Instance.gameTime));
            if (transform.localEulerAngles.y > maxAngleRotatePlus - 90)
            {
                rotatePlusExis = false;
            }
        }

    }


    void OnParticleCollision(GameObject other)
    {
        Status otherCollision = other.GetComponent<Status>();
        if (otherCollision && otherCollision.myType != myShipType)
        {
            otherCollision.TakeDamage(this.status[fireLevel - 1].damage);
        }
        else if (other.GetComponent<EnemysBehavior>())
        {
            other.GetComponent<EnemysBehavior>().TakeDamage((int)this.status[fireLevel - 1].damage);
        }

    }


    public void EnableParticleFire()
    {
        GetComponent<ParticleSystem>().Play();

        isAble = false;
        Invoke("StopParticleFire", status[fireLevel - 1].duration);
    }


    public void StopParticleFire()
    {
        GetComponent<ParticleSystem>().Stop();
        Invoke("DisableParticleFire", status[fireLevel - 1].durationRecharg);
    }


    public void DisableParticleFire()
    {
        isAble = true;
    }
}

[System.Serializable]
public class FireStatus 
{
    public float duration, durationRecharg;
    public int manaCost;
    public float damage;
    public float maxAngleToRotate, speedMovement;
}