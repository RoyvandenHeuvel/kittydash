using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    public string sceneName;

    void Awake()
    {
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
