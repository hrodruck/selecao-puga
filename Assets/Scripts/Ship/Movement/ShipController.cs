using UnityEngine;
using System.Collections.Generic;

public class ShipController : Status
{
    [Header("Reference")]
    public GameObject shield;
    public GameObject cannon;
    public GameObject cannonLocalSpawn;
    public List<SecondaryCannon> allSecondaryCannon;
    public List<DroneBehavior> allDrones;
    public MagaBombBehavior bombSpecial;
    public FireBehavior fireSpecial;
    public LazerBehavior lazerSpecial;
    public List<GameObject> Meshs;

    [Header("Settings")]
    public bool inputPC;
    public bool smoothRotateShield;
    public float shieldSpeedToSmoothRotation;
    public float angleToRotateShield;
    [Range(0, 1)] public float sensibilityInput;
    public float delayInputTimeToRotateShield;
    public float shootForSeconds;
    public BulletType bullet;
    public SecondaryCannonType mySecondaryCannonType;
    public DroneType myDroneType;
    public List<ManaStatus> mana;
    [Range(1, 3)] public int manaLevel;
    public SpecialType currentSpecialAble;
    public float slowMotionMaxTime;
    public float slowMotionTimeToFullRecharg;
    [Range(0, 1)] public float slowMotionEffect;

    [Header("Behaviour")]
    bool inStun;
    float currentStunTime;
    float stunTime;
    float currentTimeToRotateShield;
    [HideInInspector] public Vector3 targetToShoot;
    float timeToShoot;
    float currentTimeToShoot;
    SecondaryCannon currentCannon;
    [HideInInspector] public int currentManaToSpecial;
    bool slowMotionAvaliable;
    float specialRechargTime;
    float currentSpecialRechargTime;
    float currentslowMotionTime;


    void Start()
    {
        SetShootRate();
        SetDamage();
        SetCurrentSecondaryCannon(mySecondaryCannonType);
        SetCurrentDrone(myDroneType);
        SetSpecialStats();

        currentslowMotionTime = slowMotionMaxTime;
    }


    void Update()
    {
        if (!GameManager.Instance.endGame)
        {
            if (inStun)
            {
                currentStunTime += Time.deltaTime * GameManager.Instance.gameTime;
                if (currentStunTime > stunTime)
                {
                    inStun = false;
                    stunTime = 0;
                }
            }
            else
            {
                gameObject.transform.Translate(Input.GetAxis("Horizontal") * this.allStatus[speedLevel - 1].speed * Time.deltaTime * GameManager.Instance.gameTime, 0, Input.GetAxis("Vertical") * this.allStatus[speedLevel - 1].speed * Time.deltaTime * GameManager.Instance.gameTime);

                if (inputPC)
                {
                    UpdateTargetToShootPC();
                }
                else
                    UpdateTargetToShootJoystick();

                if (smoothRotateShield)
                {
                    UpdateSmoothRotateShield();
                }
                else
                {
                    UpdateRotateShield();
                }

                currentTimeToShoot += Time.deltaTime * GameManager.Instance.gameTime;

                if (Input.GetButton("Fire1"))
                {
                    Shoot();
                }

                if (Input.GetButtonDown("Slow Motion"))
                {
                    SlowMotionEneble(true);
                }

                currentSpecialRechargTime += Time.deltaTime * GameManager.Instance.gameTime;
                if (Input.GetButtonDown("Special"))
                {
                    if (currentSpecialRechargTime > specialRechargTime)
                    {
                        ActiveSpecial();
                    }
                }


                if (slowMotionAvaliable)
                {
                    SlowMotionEffect();
                }
                else if (currentslowMotionTime < slowMotionMaxTime)
                {
                    RechargSlowMotion();
                }

                if (this.allStatus[healthLevel - 1].health <= 0)
                {
                    EnebleMesh(false);
                    GameManager.Instance.EndGame();
                }
            }
        }
    }


    void SetDamage()
    {
        damage = this.allStatus[attackLevel - 1].attack * 1;
    }


    void UpdateRotateShield()
    {
        if (Input.GetAxis("RotateShield") < 0.19f && Input.GetAxis("RotateShield") > 0.19f)
        {
            currentTimeToRotateShield = delayInputTimeToRotateShield;
        }

        currentTimeToRotateShield += Time.deltaTime * GameManager.Instance.gameTime;


        if (currentTimeToRotateShield >= delayInputTimeToRotateShield)
        {
            if (Input.GetAxis("RotateShield") > sensibilityInput)
            {
                shield.transform.Rotate(new Vector3(0, -angleToRotateShield, 0));
                currentTimeToRotateShield = 0;
            }
            else if (Input.GetAxis("RotateShield") < -sensibilityInput)
            {
                shield.transform.Rotate(new Vector3(0, angleToRotateShield, 0));
                currentTimeToRotateShield = 0;
            }
        }
    }


    void UpdateSmoothRotateShield()
    {
        shield.transform.Rotate(new Vector3(0, shieldSpeedToSmoothRotation * Input.GetAxis("RotateShield") * Time.deltaTime * GameManager.Instance.gameTime, 0));
    }


    void UpdateTargetToShootPC()
    {
        float localX = 0;
        float localZ = 0;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hit;

        hit = Physics.RaycastAll(ray);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.CompareTag("Plataform") || hit[i].transform.CompareTag("Wall"))
            {
                localX = hit[i].point.x;
                localZ = hit[i].point.z;
                break;
            }
        }

        targetToShoot = new Vector3(localX, cannon.transform.position.y, localZ);

        cannon.transform.LookAt(targetToShoot);

    }


    void UpdateTargetToShootJoystick()
    {
        float localX = Input.GetAxis("Horizontal Rotation");
        float localZ = Input.GetAxis("Vertical Rotation");

        targetToShoot = new Vector3(gameObject.transform.position.x + localX, cannon.transform.position.y, gameObject.transform.position.z + localZ);
        if (targetToShoot != gameObject.transform.position)
        {
            cannon.transform.LookAt(targetToShoot);
            if (Vector2.SqrMagnitude(new Vector2(localX, localZ)) > 0.7f)
                Shoot();
        }
    }


    void Shoot()
    {
        if (currentTimeToShoot > timeToShoot)
        {
            GameObject currentBullet = InstanceManager.Instance.InstanceBullet(bullet, cannonLocalSpawn.transform.position, cannonLocalSpawn.transform.rotation);
            currentBullet.GetComponent<BulletBehavior>().SetBulletStats(damage, this.myType);


            if (currentCannon != null)
            {
                for (int i = 0; i < currentCannon.cannonPoint.Count; i++)
                {
                    GameObject currentSecondaryBullet = InstanceManager.Instance.InstanceBullet(bullet, currentCannon.cannonPoint[i].transform.position, currentCannon.cannonPoint[i].transform.rotation);
                    currentSecondaryBullet.GetComponent<BulletBehavior>().SetBulletStats((int)(damage * currentCannon.status[currentCannon.level - 1].DamageIndex), this.myType);
                }
            }

            currentTimeToShoot = 0;
        }
    }


    void SetShootRate()
    {
        timeToShoot = 1 / shootForSeconds;
    }


    void SlowMotionEffect()
    {
        currentslowMotionTime -= Time.deltaTime;

        if (currentslowMotionTime <= 0)
        {
            SlowMotionEneble(false);
        }
    }


    void RechargSlowMotion()
    {
        currentslowMotionTime += Time.deltaTime / (slowMotionTimeToFullRecharg / slowMotionMaxTime);

        if (currentslowMotionTime > slowMotionMaxTime)
            currentslowMotionTime = slowMotionMaxTime;
    }


    void SlowMotionEneble(bool enable)
    {
        if (enable)
        {
            GameManager.Instance.gameTime = slowMotionEffect;
            slowMotionAvaliable = true;
        }
        else
        {
            GameManager.Instance.gameTime = 1;
            slowMotionAvaliable = false;
        }
    }


    public void EnebleMesh(bool state)
    {
        foreach (GameObject ob in Meshs)
        {
            ob.SetActive(state);
        }
    }


    public void SetCurrentSecondaryCannon(SecondaryCannonType currentType)
    {
        for (int i = 0; i < allSecondaryCannon.Count; i++)
        {
            if (allSecondaryCannon[i].myCannonType == currentType)
            {
                currentCannon = allSecondaryCannon[i];
                allSecondaryCannon[i].gameObject.SetActive(true);
                Meshs.Add(allSecondaryCannon[i].gameObject);
            }
            else
            {
                allSecondaryCannon[i].gameObject.SetActive(false);
                if (Meshs.Contains(allSecondaryCannon[i].gameObject))
                    Meshs.Remove(allSecondaryCannon[i].gameObject);
            }
        }
    }


    public void SetCurrentDrone(DroneType currentType)
    {
        for (int i = 0; i < allDrones.Count; i++)
        {
            if (allDrones[i].thisDroneType == myDroneType)
            {
                allDrones[i].gameObject.SetActive(true);
                Meshs.Add(allDrones[i].gameObject);
            }
            else
            {
                allDrones[i].gameObject.SetActive(false);
                if (Meshs.Contains(allDrones[i].gameObject))
                    Meshs.Remove(allDrones[i].gameObject);
            }

        }
    }


    void SetSpecialStats()
    {
        switch (currentSpecialAble)
        {
            case SpecialType.BOMB:
                specialRechargTime = bombSpecial.status[bombSpecial.megaBombLevel - 1].durationRecharg;
                break;
            case SpecialType.LAZER:
                specialRechargTime = lazerSpecial.status[lazerSpecial.lazerLevel - 1].durationRecharg;
                break;
            case SpecialType.FIRE:
                specialRechargTime = fireSpecial.status[fireSpecial.fireLevel - 1].durationRecharg;
                break;
        }

        lazerSpecial.gameObject.SetActive(false);
        fireSpecial.gameObject.SetActive(false);

        currentSpecialRechargTime = specialRechargTime;
        currentManaToSpecial = mana[manaLevel].ManaTotal;
    }


    void ActiveSpecial()
    {
        switch (currentSpecialAble)
        {
            case SpecialType.BOMB:
                if (currentManaToSpecial >= bombSpecial.status[bombSpecial.megaBombLevel - 1].manaCost)
                {
                    Instantiate(bombSpecial, this.transform.position, Quaternion.identity);
                    currentSpecialRechargTime = 0;
                    currentManaToSpecial -= bombSpecial.status[bombSpecial.megaBombLevel - 1].manaCost;
                }
                break;
            case SpecialType.LAZER:
                if (currentManaToSpecial >= lazerSpecial.status[lazerSpecial.lazerLevel - 1].manaCost)
                {
                    lazerSpecial.gameObject.SetActive(true);
                    currentSpecialRechargTime = 0;
                    lazerSpecial.currentDuration = 0;
                    currentManaToSpecial -= lazerSpecial.status[lazerSpecial.lazerLevel - 1].manaCost;
                }
                break;
            case SpecialType.FIRE:
                if (currentManaToSpecial >= fireSpecial.status[fireSpecial.fireLevel - 1].manaCost)
                {
                    fireSpecial.gameObject.SetActive(true);
                    fireSpecial.currentDuration = 0;
                    currentSpecialRechargTime = 0;
                    currentManaToSpecial -= fireSpecial.status[fireSpecial.fireLevel - 1].manaCost;
                }
                break;
        }
    }


    public void EnableStun(float newStunTime)
    {
        if (newStunTime > stunTime)
        {
            stunTime = newStunTime;
        }

        currentStunTime = 0;
        inStun = true;

    }
}

[System.Serializable]
public class ManaStatus
{
    public int ManaTotal;
}