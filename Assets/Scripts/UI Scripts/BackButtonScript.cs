using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    private static BackButtonScript _instance = null;

    public static BackButtonScript Instance
    {
        get
        {
            return _instance;
        }
    }

    public string sceneName;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            KittyDashSceneManager.LoadSceneOrQuit(sceneName);       
        }
    }
}

public class KittyDashSceneManager : SceneManager
{
    public static void LoadSceneOrQuit(string sceneName)
    {
        if (!SceneManager.GetActiveScene().name.Equals(sceneName))
        {
            KittyDashSceneManager.LoadScene(sceneName);
        }
        else
        {
            Application.Quit();
        }
    }
}
