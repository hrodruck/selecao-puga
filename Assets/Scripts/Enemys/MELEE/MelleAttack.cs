using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MelleAttack : MonoBehaviour {

    [SerializeField]
    float timeToDestroy;
//    [SerializeField]
//    int maxEnemysCont;
//    [SerializeField]
//    List<GameObject> targets;

    void Start() 
    {
        Destroy(this.gameObject, timeToDestroy);
    }

//    void OnTriggerEnter() 
//    {
  //      if()
//    }
    
}
