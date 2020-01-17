using UnityEngine;
using System.Collections;

public class TowerFireAttack : MonoBehaviour {

    [HideInInspector]
    public  int damage;

    void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<ShieldBehavior>())
        {
            ShieldBehavior shield = other.GetComponent<ShieldBehavior>();
            shield.TakeDamage(damage);
        }
        else if (other.GetComponent<ShipController>())
        {
            ShipController ship = other.GetComponent<ShipController>();
            ship.TakeDamage(damage);
        }
    }
}
