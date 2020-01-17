using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;
    public static SpawnManager Instance { get { return instance; } }

    [Header("References")]
    public List<GameObject> enemys;
    public GameObject PointToSpawn;
    public Transform pivotPoint;

    [Header("Settings")]
    public float maxRadius;
    public float heightToSpawn;
    public int maxEnemys;
    public float delayTimeToSpawn;
    public List<string> ignoreTag;

    [Header("Behaviour")]
    List<GameObject> allEnemys = new List<GameObject>();
    Vector3 localToSpawn;
    public float currentTimeToSpawn;
    [HideInInspector] public bool spawnAble;
    [HideInInspector] public int currentEnemys;


    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        spawnAble = true;
        gameObject.transform.position = new Vector3(pivotPoint.position.x, 30 + heightToSpawn, pivotPoint.position.z);
    }


    void Update()
    {
        currentTimeToSpawn += Time.deltaTime * GameManager.Instance.gameTime;

        if (currentTimeToSpawn >= delayTimeToSpawn && currentEnemys < maxEnemys && spawnAble)
        {
            SpawnEnemys();
            currentTimeToSpawn = 0;
        }

    }


    void SpawnEnemys()
    {
        SetNewLocalToSpawn();

        GameObject enemyToSpawn = SelectEnemy();

        GameObject currentEnemy = (GameObject)Instantiate(enemyToSpawn, localToSpawn, Quaternion.identity);
        allEnemys.Add(currentEnemy);
        currentEnemys++;
    }


    GameObject SelectEnemy()
    {
        return enemys[Random.Range(0, enemys.Count)];
    }


    void SetNewLocalToSpawn()
    {
        PointToSpawn.transform.position = new Vector3(Random.Range(0, maxRadius), transform.position.y, transform.position.z);

        gameObject.transform.Rotate(new Vector3(0, Random.Range(1, 359), 0));

        Ray ray = new Ray(PointToSpawn.transform.position, Vector3.down);
        RaycastHit[] hit = Physics.RaycastAll(ray);

        for (int i = 0; i < hit.Length; i++)
        {
            if (!ignoreTag.Contains(hit[i].transform.tag) && !hit[i].transform.CompareTag("Plataform"))
            {
                SetNewLocalToSpawn();
            }
        }

        localToSpawn = PointToSpawn.transform.position;
        localToSpawn.y = heightToSpawn;
    }


    public void DestroyerAllEnemy()
    {
        foreach (GameObject ob in allEnemys)
        {
            Destroy(ob);
            currentEnemys--;
        }
    }
}
