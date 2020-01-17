using UnityEngine;
using System.Collections.Generic;

public class SecondaryCannon : MonoBehaviour
{
    [Header("Reference")]
    public List<Transform> cannonPoint;

    [Header("Settings")]
    public SecondaryCannonType myCannonType;
    public List<SecondaryCannonStatus> status;
    [Range(1, 5)] public int level = 1;
}


[System.Serializable]
public class SecondaryCannonStatus
{
    public float DamageIndex;
}