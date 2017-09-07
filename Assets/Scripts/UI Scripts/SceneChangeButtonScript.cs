using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangeButtonScript : MonoBehaviour
{
    public Button @Button;
    public string SceneName;

    void Start()
    {
        @Button = GetComponent<Button>();
        @Button.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
}
