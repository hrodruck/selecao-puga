using UnityEngine;
using System.Collections;

public class StaticMoveBehavior : Status
{

    [HideInInspector]
    public bool isAble;

    GameObject currentShip;

    public GameObject rechargManaDrop;

    public int myMoneyToDrop;
    public float MaxDistanceToDrop;

    void Start()
    {
        damage = this.allStatus[attackLevel - 1].attack * 2;
    }


    void Update()
    {
        if (isAble)
        {
            //Vector3 target = new Vector3(currentShip.transform.position.x, this.gameObject.transform.position.y, currentShip.transform.position.z);
            //gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, target, this.speed * Time.deltaTime  * GameManager.Instance.gameTime);
            Move();
        }

        if (this.allStatus[healthLevel - 1].health <= 0)
        {
            isAble = false;
            CheckMoney();
            Death(0.4f,rechargManaDrop);
            SpawnManager.Instance.currentEnemys--;
        }
    }


    void Move()
    {

        //   gameObject.transform.Translate((currentShip.transform.position.x - transform.position.x) * this.speed * Time.deltaTime * GameManager.Instance.gameTime, 0, (currentShip.transform.position.z - transform.position.z) * this.speed * Time.deltaTime * GameManager.Instance.gameTime);
        gameObject.transform.position = Vector3.LerpUnclamped(transform.position, currentShip.transform.position, -(this.allStatus[speedLevel - 1].speed - (Vector3.Distance(currentShip.transform.position, transform.position))) * Time.deltaTime * GameManager.Instance.gameTime);
    }


    public void SetActive(bool active, GameObject ship)
    {
        isAble = active;
        currentShip = ship;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ship"))
        {
            other.GetComponent<Status>().TakeDamage(damage);
            CheckMoney();
            Death(0.3f, rechargManaDrop);
        }
        else if (other.transform.CompareTag("Shield"))
        {
            other.GetComponent<ShieldBehavior>().TakeDamage(damage);
            CheckMoney();
            Death(0.3f, rechargManaDrop);
        }
    }


    // void OnTriggerStay(Collider other)
    // {
    //     if (!other.transform.CompareTag("Arena")) 
    //     {
    //          Death(0.4f,rechargManaDrop);
    //     }
    // }


    void CheckMoney()
    {
        if (myMoneyToDrop > 0)
        {
            int numberCoin = (int)Mathf.Floor(myMoneyToDrop / CurrencyManager.Instance.maxCountForCurrencys);
            int restMoneyToLastCoin = 0;

            if (myMoneyToDrop - ((int)Mathf.Floor(myMoneyToDrop / CurrencyManager.Instance.maxCountForCurrencys) * CurrencyManager.Instance.maxCountForCurrencys) > 0)
            {
                restMoneyToLastCoin = myMoneyToDrop - (int)Mathf.Floor(myMoneyToDrop / CurrencyManager.Instance.maxCountForCurrencys);
            }
            else
            {
                DropMoney(numberCoin);
                return;
            }

            DropMoney(numberCoin, true, restMoneyToLastCoin);

            myMoneyToDrop = 0;
        }
    }


    void DropMoney(int coinsToDrop, bool rest = false, int restValue = 0)
    {
        for (int i = 0; i < coinsToDrop; i++)
        {
            Vector3 RandomLocal = new Vector3(transform.position.x + Random.Range(0f, MaxDistanceToDrop), transform.position.y, transform.position.z + Random.Range(0f, MaxDistanceToDrop));
            GameObject coin = (GameObject)Instantiate(CurrencyManager.Instance.coinMesh, RandomLocal, Quaternion.identity);
            coin.GetComponent<CurrencyBehaviour>().SetValue(CurrencyManager.Instance.maxCountForCurrencys);
        }

        if (rest)
        {
            Vector3 RandomLocal = new Vector3(transform.position.x + Random.Range(0f, MaxDistanceToDrop), transform.position.y, transform.position.z + Random.Range(0f, MaxDistanceToDrop));
            GameObject coin = (GameObject)Instantiate(CurrencyManager.Instance.coinMesh, RandomLocal, Quaternion.identity);
            coin.GetComponent<CurrencyBehaviour>().SetValue(restValue);
        }
    }

}
