using UnityEngine;

public class ManaEnergy : MonoBehaviour
{
    [Header("Settings")]
    public int manaRechargValue;
    public float timeToDestroy;


    void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ship"))
        {
            if (RechargMana(other.GetComponent<ShipController>()))
                Destroy(this.gameObject);
        }
    }


    bool RechargMana(ShipController otherShip)
    {
        if (otherShip.currentManaToSpecial >= otherShip.mana[otherShip.manaLevel].ManaTotal)
            return false;

        otherShip.currentManaToSpecial += manaRechargValue;
        Mathf.Clamp(otherShip.currentManaToSpecial, 0, otherShip.mana[otherShip.manaLevel].ManaTotal);

        return true;
    }
}
