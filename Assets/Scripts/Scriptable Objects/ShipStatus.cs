using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Ship Status", menuName ="Ship Status")]
public class ShipStatus : ScriptableObject
{
    public int life;
    public int fireDamage;
    public int bulletsToRecharg;
    public int shieldResistence;
    public int meleeDamage;
    public float movimentSpeed;
    public float fireRate;
    public float fireRechargTime;
    public float shieldRechargTime;
    public float meleeAttackDelay;
    public float meleeAttackTime;
    public float stunTime;
}
