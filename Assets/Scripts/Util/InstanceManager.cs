using UnityEngine;

public class InstanceManager : MonoBehaviour
{
    [Header("Singleton")]
    public static InstanceManager instance;
    public static InstanceManager Instance { get { return instance; } }

    [Header("References")]
    public GameObject normalBullet;
    public GameObject rocketBullet;


    void Awake()
    {
        instance = this;
    }


    public GameObject InstanceBullet(BulletType Bullet, Vector3 currentPosition, Quaternion currentRotation)
    {
        GameObject model;
        switch (Bullet)
        {
            default:
            case BulletType.NORMAL:
                model = normalBullet;
                break;

            case BulletType.ROCKET:
                model = rocketBullet;
                break;
        }

        if (model != null)
            return (GameObject)Instantiate(model, currentPosition, currentRotation);
        else return null;
    }


    public void DissolvedBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
