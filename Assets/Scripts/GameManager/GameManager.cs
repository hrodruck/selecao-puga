using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [Header("References")]
    public Transform pivotToRestart;
    public GameObject ship;

    [Header("Behaviour")]
    [HideInInspector] public float gameTime = 1;
    [HideInInspector] public bool endGame;
    [HideInInspector] public bool victory;


    void Awake()
    {
        instance = this;
    }


    void Update()
    {
        if (Input.GetButton("Cancel") && endGame)
            RestatGame();
        else if (Input.GetButton("Submit") && endGame)
        {
            SceneManager.Instance.resetToHome();
        }
    }


    public void EndGame(bool playerWon)
    {
        endGame = true;
        victory = playerWon;
        SpawnManager.Instance.spawnAble = false;
        SpawnManager.Instance.DestroyerAllEnemy();
        gameTime = 0;
    }


    void RestatGame()
    {
        endGame = false;
        ship.transform.position = new Vector3(pivotToRestart.position.x, ship.transform.position.y, pivotToRestart.position.z);
        ship.GetComponent<ShipController>().allStatus[ship.GetComponent<ShipController>().healthLevel - 1].health = 100;
        ship.GetComponent<ShipController>().EnebleMesh(true);
        SpawnManager.Instance.DestroyerAllEnemy();
        SpawnManager.Instance.spawnAble = true;
        UIManager.Instance.resetUI();
        CurrencyManager.Instance.totalCurrencys = 0;
        gameTime = 1;
    }
}
