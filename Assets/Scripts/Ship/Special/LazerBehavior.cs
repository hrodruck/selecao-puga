using UnityEngine;
using System.Collections.Generic;

public class LazerBehavior : MonoBehaviour
{
    [Header("Settings")]
    public ShipType myShipType;
    public List<LazerStatus> status;
    [Range(1, 3)] public int lazerLevel;

    [Header("Behaviour")]
    bool isAble = false;
    [HideInInspector] public float currentDuration;


    void Update() 
    {
        currentDuration += Time.deltaTime * GameManager.Instance.gameTime;
        if (currentDuration > status[lazerLevel-1].duration) 
        {
            this.gameObject.SetActive(false);
        }
    }


    void OnParticleCollision(GameObject other) 
    {
        Status otherCollision = other.GetComponent<Status>();

        if (otherCollision && otherCollision.myType != myShipType)
            {
                otherCollision.TakeDamage(this.status[lazerLevel - 1].damage);
            }
        else if (other.GetComponent<EnemysBehavior>())
        {
            other.GetComponent<EnemysBehavior>().TakeDamage((int)this.status[lazerLevel - 1].damage);
        }
    }


    public void EnableParticleLazer() 
    {
        GetComponent<ParticleSystem>().Play();

        isAble = false;
        Invoke("StopParticleLazer", status[lazerLevel - 1].duration);
    }


    public void StopParticleLazer() 
    {
        GetComponent<ParticleSystem>().Stop();
        Invoke("DisableParticleLazer", status[lazerLevel - 1].durationRecharg);
    }


    public void DisableParticleLazer() 
    {
        isAble = true;
    }
}

[System.Serializable]
public class LazerStatus 
{
    public float duration, durationRecharg;
    public int manaCost;
    public float damage;
}
