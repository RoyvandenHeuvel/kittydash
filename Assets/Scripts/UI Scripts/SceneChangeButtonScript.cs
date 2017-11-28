using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangeButtonScript : MonoBehaviour
{
    private Button @Button;
    public string SceneName;

    void Start()
    {
        @Button = GetComponent<Button>();
        @Button.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        if (SceneName.Equals("scene_tutorial") && GameManager.Instance.SaveData.HideTutorials)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Ingame");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        }
    }
}
