using UnityEngine;
using System.Collections.Generic;

public class ShieldBehavior : MonoBehaviour
{
    [Header("References")]
    public ShipController myShip;
    public List<GameObject> myMesh;

    [Header("Settings")]
    public List<ShieldStatus> status;
    [Range(1, 3)] public int shieldSizeLevel;
    [Range(1, 3)] public int shieldResistenceLevel;
    [Range(1, 3)] public int shieldReloadLevel;

    [Header("Behaviour")]
    Collider myCollider;
    bool isAble = true;
    int currentShielResistence;
    float currentTime;


    void Start()
    {
        myCollider = GetComponent<Collider>();
        currentShielResistence = status[shieldResistenceLevel - 1].shieldResistence;
    }


    void Update()
    {
        if (!isAble)
            Recharg();
    }


    public void Disable()
    {
        isAble = false;
        foreach (GameObject ob in myMesh)
            ob.SetActive(false);
        this.myCollider.enabled = false;
        currentTime = 0;
    }


    public void Recharg()
    {
        currentTime += Time.deltaTime * GameManager.Instance.gameTime;

        if (currentTime >= status[shieldReloadLevel - 1].shieldReload)
        {
            isAble = true;
            currentShielResistence = status[shieldResistenceLevel - 1].shieldResistence;
            foreach (GameObject ob in myMesh)
                ob.SetActive(true);
            this.myCollider.enabled = true;
        }
    }


    public void TakeDamage(float damage)
    {
        currentShielResistence -= (int)damage;

        if (currentShielResistence <= 0)
        {
            Disable();
        }
    }
}

[System.Serializable]
public class ShieldStatus
{
    public int shieldSize;
    public int shieldResistence;
    public float shieldReload;
}
