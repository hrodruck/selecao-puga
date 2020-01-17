using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public BulletType bulletType;


    protected void DestroyBullet() 
    {
        InstanceManager.Instance.DissolvedBullet(this);
    }
}
