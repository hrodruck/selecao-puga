using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [Header("Singleton")]
    public static SceneManager instance;
    public static SceneManager Instance { get { return instance; } }

    private void Awake()
    {
        if (SceneManager.Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
    public void resetToHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
